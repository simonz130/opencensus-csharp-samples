// Copyright 2019 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace GettingStarted_Metrics
{
    using OpenCensus.Exporter.Stackdriver;
    using OpenCensus.Stats;
    using OpenCensus.Stats.Aggregations;
    using OpenCensus.Stats.Measures;
    using OpenCensus.Tags;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    class StackdriverSample
    {
        // 1. Define what do we measure (including units)
        static IMeasureLong numberOfInvocations =
            MeasureLong.Create("foo_execution_size", "Number of Foo method invocations", "1");

        // 2. Define tag keys. Every measurement will be tagged with these tags
        static ITagKey tagEnv = TagKey.Create("environment");
        static ITagKey tagMachine = TagKey.Create("machine");

        // 3. Set values for tags (metadata). These will be recorded alongside the measurements
        // so you can slice and dice collected metrics by tags
        static ITagContext tagContext = Tags.Tagger.CurrentBuilder
            .Put(tagEnv, TagValue.Create("test_env"))
            .Put(tagMachine, TagValue.Create(Environment.MachineName))
            .Build();

        // 4. Component for recording metrics
        IStatsRecorder statsRecorder = Stats.StatsRecorder;

        public static void Run(string projectId)
        {
            // 5. Start Stackdriver Exporter for metrics collection
            var exporter = new StackdriverExporter(
                projectId,
                exportComponent: null,
                viewManager: Stats.ViewManager);
            exporter.Start();

            // 6. Define what will be exported (schema): a measure, how it's aggregated and how it will be tagged (metadata)
            var executionCountView = View.Create(
                ViewName.Create("sample_execution_count"),
                "Counts the number of method invocations over time",
                numberOfInvocations,
                Count.Create(),
                new List<ITagKey>() { tagEnv, tagMachine });

            // 7. Register the view to export
            Stats.ViewManager.RegisterView(executionCountView);

            // Run instrumented fragment on a separate thread
            Task.Run((Action)InstrumentedFragment);
        }

        static void InstrumentedFragment()
        {
            Random r = new Random();

            while (true)
            {
                int sleepTime = r.Next(300, 1000);
                Thread.Sleep(sleepTime);

                // Actual method invocation
                Foo();

                // 8. Record method invocation
                Stats.StatsRecorder.NewMeasureMap()
                    .Put(numberOfInvocations, 1)
                    .Record(tagContext);
            }
        }

        static void Foo()
        {
            Console.Write(".");
        }
    }
}
