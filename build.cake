
// #tool "nuget:?package=nuget.commandline&version=5.3.0"
// #addin nuget:?package=Cake.Svn&version=0.9.0
#r "Cake.Comon.dll"

var target = Argument("target", "ExecuteBuild");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./";
var myLibraryFolder= "./MyLibrary";
var outputFolder = "./artifacts";
var myLibraryOutputfolder= "./myLibraryArtifacts";

Task("Clean")
    .Does(()=>{
        CleanDirectory(outputFolder);
		CleanDirectory(myLibraryOutputfolder);
    });


Task("Restore")
    .Does(()=> {
        DotNetCoreRestore(solutionFolder);
    });
Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() => {

		ReplaceAppSetting("App.config", "UseExportLC", "ifs.demo.com");

		DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings {
			NoRestore = true,
			Configuration = configuration
		});
	});

Task("Test")
	.IsDependentOn("Build")
	.Does(()=> {
		DotNetCoreTest(solutionFolder, new DotNetCoreTestSettings {
			NoRestore = true,
			Configuration = configuration,
			NoBuild = true
		});
	});

Task("Publish")
	.IsDependentOn("Test")
	.Does(()=> {
		DotNetCorePublish(solutionFolder, new DotNetCorePublishSettings
		{
			NoRestore = true,
			Configuration = configuration,
			NoBuild = true,
			OutputDirectory = outputFolder
		});
	});

Task("PublishLibrary")
	.IsDependentOn("Test")
	.Does(()=> {
		DotNetCorePublish(myLibraryFolder, new DotNetCorePublishSettings
		{
			NoRestore = true,
			Configuration = configuration,
			NoBuild = true,
			OutputDirectory = myLibraryOutputfolder
		});
		Zip("publish", $"{outputFolder}/zip");
	});
Task("ExecuteBuild")
	.IsDependentOn("Publish")
	.IsDependentOn("PublishLibrary");

 RunTarget(target);   