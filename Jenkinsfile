pipeline {
    agent {
        docker { 
      
          image 'mcr.microsoft.com/dotnet/framework/sdk:4.8'
          label 'windows'
        }
        
    }
    stages {
         stage('Restore nuget') {
            steps {
              
              
                bat 'dotnet restore' //for .NET core
                bat 'nuget restore BigProject.sln' // for .NET framework
               
            }
        }
        stage('Build') {
            steps {
               
                bat 'dotnet build --configuration Release ./BigProject.NetCoreApp/BigProject.NetCoreApp.csproj'           
                bat 'msbuild BigProject.sln /target:BigProject_NetFrameworkApp /p:Configuration=Release'
               
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test --logger trx ./BigProject.NetCoreApp.Test/BigProject.NetCoreApp.Test.csproj'
            }
             post {
                    always {
                      //plugin: https://plugins.jenkins.io/mstest/
                        mstest testResultsFile:"**/*.trx", keepLongStdio: true
                    }
        
                }           
        }
        
        stage('Deploy') {
            steps {
                bat 'mkdir deploy'
                bat 'dotnet publish --self-contained --runtime win-x64 -c Release ./BigProject.NetCore/BigProject.NetCore.csproj -o ./deploy/BigProject.NetCore'            
           	    //NOT BE TESTED YET
                bat 'msbuild BigProject.sln /target:BigProject_NetFrameworkApp /p:Configuration=Release /p:DeployOnBuild=True /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:DeleteExistingFiles=True /p:publishUrl=./deploy/BigProject.NetFrameworkApp'
                archiveArtifacts artifacts: 'deploy/**', fingerprint: true
                
            }
        }
    }
}
