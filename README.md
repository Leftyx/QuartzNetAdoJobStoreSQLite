QuartzNetAdoJobStoreSQLite
==========================

Quartz.Net using Sqlite as DataStore

Configuration

<add key="quartz.threadPool.type" value="Quartz.Simpl.SimpleThreadPool, Quartz" />
<add key="quartz.threadPool.threadCount" value="10" />
<add key="quartz.jobStore.type" value="Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" />
<add key="quartz.jobStore.misfireThreshold" value="60000" />
<add key="quartz.jobStore.lockHandler.type" value="Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz" />
<add key="quartz.jobStore.useProperties" value="true" />
<add key="quartz.jobStore.dataSource" value="default" />
<add key="quartz.jobStore.tablePrefix" value="qrtz_" />
<add key="quartz.jobStore.driverDelegateType" value="Quartz.Impl.AdoJobStore.SQLiteDelegate, Quartz" />
<add key="quartz.dataSource.default.provider" value="SQLite-10" />
<add key="quartz.dataSource.default.connectionString" value="Data Source=data\quartznet.db;Version=3;" />

Install ADO.NET Sqlite

Install-Package System.Data.SQLite.Core

Binding

<runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
        <dependentAssembly>
            <assemblyIdentity name="System.Data.SQLite" publicKeyToken="db937bc2d44ff139" culture="neutral" />
            <bindingRedirect oldVersion="1.0.88.0" newVersion="1.0.94.0" />
        </dependentAssembly>
    </assemblyBinding>
</runtime>
