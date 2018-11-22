// [!] DO NOT REMOVE THIS FILE

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Scripting;

#if NETFX_CORE
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Storage;
#endif

namespace GameFlow {

[Preserve]
public class RuntimeReflector : IReflector {

[RuntimeInitializeOnLoadMethod]
static void Init() {
	RuntimeReflection.reflector = new RuntimeReflector();
}

private RuntimeReflector() {
}

#if NETFX_CORE
private static Assembly[] _assemblies;
#endif

public Assembly[] GetAssemblies() {
	#if NETFX_CORE
    if (_assemblies != null) {
        return _assemblies;
    }
    StorageFolder folder = Package.Current.InstalledLocation;
	var task = folder.GetFilesAsync().AsTask();
    List<Assembly> list = new List<Assembly>();
    foreach (StorageFile file in task.Result) {
        if (file.FileType == ".dll" || file.FileType == ".exe") {
            try {
                var filename = file.Name.Substring(0, file.Name.Length - file.FileType.Length);
                AssemblyName name = new AssemblyName { Name = filename };
                Assembly assembly = Assembly.Load(name);
                list.Add(assembly);
            } catch {
            }
        }
    }
	_assemblies = list.ToArray();
    return _assemblies;	
	#else
	return AppDomain.CurrentDomain.GetAssemblies();
	#endif
}

public bool IsTypeArray(Type type) {
	#if NETFX_CORE
    return type.GetTypeInfo().IsArray;
	#else
	return type.IsArray;
	#endif
}

public bool IsTypePrimitive(Type type) {
	#if NETFX_CORE
    return type.GetTypeInfo().IsPrimitive;
	#else
	return type.IsPrimitive;
	#endif
}

public bool IsTypeVisible(Type type) {
	#if NETFX_CORE
    return type.GetTypeInfo().IsVisible;
	#else
	return type.IsVisible;
	#endif
}

public bool IsTypeEnum(Type type) {
	#if NETFX_CORE
    return type.GetTypeInfo().IsEnum;
	#else
	return type.IsEnum;
	#endif
}

public bool IsTypeValueType(Type type) {
	#if NETFX_CORE
    return type.GetTypeInfo().IsValueType;
	#else
	return type.IsValueType;
	#endif
}

public bool IsTypeSubclassOf(Type type1, Type type2) {
	#if NETFX_CORE
    return type1.GetTypeInfo().IsSubclassOf(type2);
	#else
	return type1.IsSubclassOf(type2);
	#endif
}

public bool IsTypeAssignableFrom(Type type1, Type type2) {
	#if NETFX_CORE
    return type1.GetTypeInfo().IsAssignableFrom(type2.GetTypeInfo());
	#else
	return type1.IsAssignableFrom(type2);
	#endif
}

public PropertyInfo GetTypeProperty(Type type, string name) {
#if NETFX_CORE
	return type.GetRuntimeProperty(name);
#else
	return type.GetProperty(name);
#endif
}

public PropertyInfo[] GetTypeProperties(Type type) {
#if NETFX_CORE
	return type.GetRuntimeProperties().ToArray();
#else
	return type.GetProperties();
#endif
}

public FieldInfo GetTypeField(Type type, string name) {
#if NETFX_CORE
	return type.GetRuntimeField(name);
#else
	return type.GetField(name);
#endif
}

public FieldInfo[] GetTypeFields(Type type) {
#if NETFX_CORE
	return type.GetRuntimeFields().ToArray();
#else
	return type.GetFields();
#endif
}

public MethodInfo GetTypeMethod(Type type, string name, Type[] paramTypes) {
#if NETFX_CORE
	return type.GetRuntimeMethod(name, paramTypes);
#else
	return type.GetMethod(name, paramTypes);
#endif
}

public MethodInfo[] GetTypeMethods(Type type) {
#if NETFX_CORE
	return type.GetRuntimeMethods().ToArray();
#else
	return type.GetMethods();
#endif
}

}

}
