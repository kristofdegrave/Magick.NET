#==================================================================================================
# Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in 
# compliance with the License. You may obtain a copy of the License at
#
#   http://www.imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
# express or implied. See the License for the specific language governing permissions and
# limitations under the License.
#==================================================================================================
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
$scriptPath = "$scriptPath\.."
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\Build.ps1
. Tools\Scripts\Shared\Config.ps1
. Tools\Scripts\Shared\GzipAssembly.ps1
. Tools\Scripts\Shared\Publish.ps1

function FixStrongNameForNet20()
{
  $fileName = "C:\Program Files (x86)\MSBuild\Microsoft.Cpp\v4.0\Platforms\Win32\Microsoft.Cpp.Win32.targets"
  $content = [IO.File]::ReadAllText($fileName)
  $content = $content.Replace('="%(Link.DelaySign)"', '="$(LinkDelaySign)"').Replace('="%(Link.KeyFile)"', '="$(LinkKeyFile)"')

  [IO.File]::WriteAllText($fileName, $content)
}

function AppVeyorBuild($quantum, $platform, $version)
{
  if ($platform -eq "AnyCPU")
  {
    AppVeyorBuild $quantum "x86" $version
    AppVeyorBuild $quantum "x64" $version

    if ($quantum -eq "Q8")
    {
      GzipAssembliesQ8
    }
    elseif ($quantum -eq "Q16")
    {
      GzipAssembliesQ16
    }
    else
    {
      GzipAssembliesQ16HDRI
    }
  }

  $builds = GetBuilds $quantum $platform

  UpdateResourceFiles $builds $version

  foreach ($build in $builds)
  {
    Build $build
  }
}

$quantum = $args[0]
$platform = $args[1]

$version = GetDevVersion
FixStrongNameForNet20
UpdateAssemblyInfos $version
AppVeyorBuild $quantum $platform $version