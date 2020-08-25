﻿// Copyright 2020 Google LLC
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

// [START functions_helloworld_storage]
using CloudNative.CloudEvents;
using Google.Cloud.Functions.Framework;
using Google.Events.Protobuf.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HelloGcs
{
    public class Function : ICloudEventFunction<StorageObjectData>
    {
        private readonly ILogger _logger;

        public Function(ILogger<Function> logger) =>
            _logger = logger;
       
        public Task HandleAsync(CloudEvent cloudEvent, StorageObjectData data, CancellationToken cancellationToken)
        {
            if (cloudEvent.Type == StorageObjectData.FinalizedCloudEventType)
            {
                // Default event type for GCS-triggered functions
                _logger.LogInformation("File {name} uploaded", data.Name);
            }
            else
            {
                _logger.LogWarning("Unsupported event type: {type}", cloudEvent.Type);
            }
            return Task.CompletedTask;
        }
    }
}
// [END functions_helloworld_storage]
