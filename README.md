<h1 align="center">Net Standard Cache Helper</h1>

<div align="center">
    
<b>Cache helper for Net Standard libraries</b>
    
[![Build Status](https://dev.azure.com/kbrashears5/github/_apis/build/status/kbrashears5.net-standard-cache-helper?branchName=master)](https://dev.azure.com/kbrashears5/github/_build/latest?definitionId=25&branchName=master)
[![Tests](https://img.shields.io/azure-devops/tests/kbrashears5/github/25)](https://img.shields.io/azure-devops/tests/kbrashears5/github/25)
[![Code Coverage](https://img.shields.io/azure-devops/coverage/kbrashears5/github/25)](https://img.shields.io/azure-devops/coverage/kbrashears5/github/25)

[![nuget](https://img.shields.io/nuget/v/NetStandardCacheHelper.svg)](https://www.nuget.org/packages/NetStandardCacheHelper/)
[![nuget](https://img.shields.io/nuget/dt/NetStandardCacheHelper)](https://img.shields.io/nuget/dt/NetStandardCacheHelper)
</div>

## Usage
```c#
var cacheNames = new List<string>() { "cache1", "cache2" };

var helper = new CacheHelper(cacheNames: cacheNames);

helper.Add<string>(cacheName: "cache1",
    key: "key",
    value: "value");
```