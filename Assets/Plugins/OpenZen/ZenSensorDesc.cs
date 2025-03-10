//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ZenSensorDesc : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ZenSensorDesc(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ZenSensorDesc obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ZenSensorDesc() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          OpenZenPINVOKE.delete_ZenSensorDesc(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public string name {
    set {
      OpenZenPINVOKE.ZenSensorDesc_name_set(swigCPtr, value);
    } 
    get {
      string ret = OpenZenPINVOKE.ZenSensorDesc_name_get(swigCPtr);
      return ret;
    } 
  }

  public string serialNumber {
    set {
      OpenZenPINVOKE.ZenSensorDesc_serialNumber_set(swigCPtr, value);
    } 
    get {
      string ret = OpenZenPINVOKE.ZenSensorDesc_serialNumber_get(swigCPtr);
      return ret;
    } 
  }

  public string ioType {
    set {
      OpenZenPINVOKE.ZenSensorDesc_ioType_set(swigCPtr, value);
    } 
    get {
      string ret = OpenZenPINVOKE.ZenSensorDesc_ioType_get(swigCPtr);
      return ret;
    } 
  }

  public string identifier {
    set {
      OpenZenPINVOKE.ZenSensorDesc_identifier_set(swigCPtr, value);
    } 
    get {
      string ret = OpenZenPINVOKE.ZenSensorDesc_identifier_get(swigCPtr);
      return ret;
    } 
  }

  public uint baudRate {
    set {
      OpenZenPINVOKE.ZenSensorDesc_baudRate_set(swigCPtr, value);
    } 
    get {
      uint ret = OpenZenPINVOKE.ZenSensorDesc_baudRate_get(swigCPtr);
      return ret;
    } 
  }

  public ZenSensorDesc() : this(OpenZenPINVOKE.new_ZenSensorDesc(), true) {
  }

}
