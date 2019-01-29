﻿// Copyright 2019 Google LLC
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

using System;

namespace GettingStarted_Metrics
{
    class Program
    {
        static void Main(string[] args)
        {
			// Set Google Cloud projectId below
            string projectId = "aspnetcoreissue";

            Console.WriteLine("Each . represents unit of work that's done and instrumented");
            Console.WriteLine($"Navigate to https://console.cloud.google.com/monitoring?project={projectId}!");
            StackdriverSample.Run(projectId);

            Console.Read();
        }
    }
}
