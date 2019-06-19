#以微软asp.net core镜像为基础
FROM microsoft/dotnet:2.2-aspnetcore-runtime

#设置镜像的工作目录为/app
WORKDIR /app

#复制当前项目的全部文件到镜像的目录/app
COPY . /app

#设置容器对外暴露端口5000
EXPOSE 5000

#设置镜像的执行命令为dotnet, 命令的参数是MedicalWeb.dll, 启动asp.net core网站
ENTRYPOINT ["dotnet", "MedicalWeb.dll"]

