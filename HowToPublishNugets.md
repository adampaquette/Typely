# How to publish packages

1. Increment `Typely.Core`
1. Commit
1. Wait for nuget to index the package
1. Increment `Typely.AspNetCore`, `Typely.AspNetCore.Swashbuckle` and `Typely.EfCore`
1. Update references to `Typely.Core` in csprojs.
1. Commit
1. Merge if all successful