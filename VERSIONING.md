# Versioning and Publishing Guide

## Version Strategy

We follow [Semantic Versioning 2.0.0](https://semver.org/) for all packages:

- **Major** version (`X.0.0`): Breaking changes
- **Minor** version (`X.Y.0`): New features, no breaking changes
- **Patch** version (`X.Y.Z`): Bug fixes and minor improvements

## Package Dependencies

Typely packages are published in a specific order to ensure dependency consistency:

1. `Typely.Core`: Base package (published first)
2. `Typely.Generators`: Source generators (published after Core)
3. Dependent packages (published after indexing delay):
   - `Typely.EfCore`
   - `Typely.AspNetCore`
   - `Typely.AspNetCore.Swashbuckle`

## Security Setup

Before publishing packages, ensure the following security measures are in place:

1. **Branch Protection Rules**:
   - Go to Repository Settings > Branches
   - Add rule for `main` branch
   - Enable "Require pull request reviews before merging"
   - Enable "Require status checks to pass before merging"
   - Enable "Require linear history"

2. **Repository Secrets**:
   - Add `NUGET_API_KEY` secret with your NuGet API key
   - Add `CODECOV_TOKEN` for coverage reports

3. **Access Control**:
   - Only organization members can create releases
   - Only approved maintainers can merge to main
   - Only @typely.io email addresses are authorized to publish

## Publishing Process

### Automated Publishing

1. Update version in `Directory.Build.props`:
   ```xml
   <PropertyGroup>
       <VersionPrefix>9.0.0</VersionPrefix>
   </PropertyGroup>
   ```

2. Commit changes and create a tag:
   ```bash
   git tag v9.0.0
   git push origin v9.0.0
   ```

3. The GitHub Action will automatically:
   - Build and test the solution
   - Verify publisher authorization
   - Verify version consistency
   - Publish packages in sequence:
     1. Typely.Core (with 60s delay for NuGet indexing)
     2. Typely.Generators (with 30s delay)
     3. All dependent packages
   - Create a GitHub Release

### Version Numbers

- **Release Versions**: `9.0.0`, `9.1.0`, `9.0.1`, etc.
- **Preview Versions**: Automatically suffixed with `-preview` for Debug builds

### Breaking Changes

When introducing breaking changes:

1. Increment the major version
2. Update the changelog
3. Update documentation to reflect changes
4. Create a migration guide if necessary

## Development Workflow

1. Make your changes in a feature branch
2. Update tests and documentation
3. Create a pull request
4. Once approved and merged, create a new version tag if needed

## Verification

After publishing:
1. Verify packages are available on [NuGet.org](https://www.nuget.org/packages?q=Typely)
2. Check [GitHub Releases](https://github.com/typely-io/Typely/releases)
3. Verify package installation in a test project
4. Verify package dependencies are correctly resolved

## Troubleshooting

If a package is not immediately available after publishing:
1. Check status on [NuGet.org](https://www.nuget.org)
2. Wait for complete indexing (can take up to 5 minutes)
3. Check GitHub Actions workflow logs
4. Verify package dependencies in the NuGet feed

## Security Measures

The publishing process includes several security checks:
1. Only authorized @typely.io members can publish
2. Version tag must match Directory.Build.props
3. Strict version format validation
4. Multiple retry attempts with proper delays
5. Automated rollback on failure
