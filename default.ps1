properties {
$base_dir = Resolve-Path .
$build_dir = "$base_dir\build"
$sln_file = "ProtoTime.sln"
$debug_dir = "$build_dir\Debug\\"
$release_dir = "$build_dir\Release\\";
}

Task default -Depends Build

Task Build{
msbuild $sln_file "/nologo" "/t:Build" "/p:Configuration=Debug" "/p:OutDir=""$debug_dir"""
}

Task Debug -depends Init {
msbuild $sln_file "/nologo" "/t:Rebuild" "/p:Configuration=Debug" "/p:OutDir=""$debug_dir"""
}

Task Release -depends Init {
msbuild $sln_file "/nologo" "/t:Rebuild" "/p:Configuration=Release" "/p:OutDir=""$release_dir"""
}