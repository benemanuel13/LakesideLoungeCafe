

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 03:14:07 2038
 */
/* Compiler settings for LakesideLoungeOffice.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __LakesideLoungeOffice_h_h__
#define __LakesideLoungeOffice_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef ___IDTExtensibility2_FWD_DEFINED__
#define ___IDTExtensibility2_FWD_DEFINED__
typedef interface _IDTExtensibility2 _IDTExtensibility2;

#endif 	/* ___IDTExtensibility2_FWD_DEFINED__ */


#ifndef __IRibbonExtensibility_FWD_DEFINED__
#define __IRibbonExtensibility_FWD_DEFINED__
typedef interface IRibbonExtensibility IRibbonExtensibility;

#endif 	/* __IRibbonExtensibility_FWD_DEFINED__ */


#ifndef __IReport_FWD_DEFINED__
#define __IReport_FWD_DEFINED__
typedef interface IReport IReport;

#endif 	/* __IReport_FWD_DEFINED__ */


#ifndef ___IDTExtensibility2_FWD_DEFINED__
#define ___IDTExtensibility2_FWD_DEFINED__
typedef interface _IDTExtensibility2 _IDTExtensibility2;

#endif 	/* ___IDTExtensibility2_FWD_DEFINED__ */


#ifndef __IAddin_FWD_DEFINED__
#define __IAddin_FWD_DEFINED__
typedef interface IAddin IAddin;

#endif 	/* __IAddin_FWD_DEFINED__ */


#ifndef __Report_FWD_DEFINED__
#define __Report_FWD_DEFINED__

#ifdef __cplusplus
typedef class Report Report;
#else
typedef struct Report Report;
#endif /* __cplusplus */

#endif 	/* __Report_FWD_DEFINED__ */


#ifndef __Addin_FWD_DEFINED__
#define __Addin_FWD_DEFINED__

#ifdef __cplusplus
typedef class Addin Addin;
#else
typedef struct Addin Addin;
#endif /* __cplusplus */

#endif 	/* __Addin_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_LakesideLoungeOffice_0000_0000 */
/* [local] */ 

typedef /* [uuid] */  DECLSPEC_UUID("289E9AF1-4973-11D1-AE81-00A0C90F26F4") 
enum ext_ConnectMode
    {
        ext_cm_AfterStartup	= 0,
        ext_cm_Startup	= 1,
        ext_cm_External	= 2,
        ext_cm_CommandLine	= 3
    } 	ext_ConnectMode;

typedef /* [uuid] */  DECLSPEC_UUID("289E9AF2-4973-11D1-AE81-00A0C90F26F4") 
enum ext_DisconnectMode
    {
        ext_dm_HostShutdown	= 0,
        ext_dm_UserClosed	= 1
    } 	ext_DisconnectMode;



extern RPC_IF_HANDLE __MIDL_itf_LakesideLoungeOffice_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_LakesideLoungeOffice_0000_0000_v0_0_s_ifspec;

#ifndef ___IDTExtensibility2_INTERFACE_DEFINED__
#define ___IDTExtensibility2_INTERFACE_DEFINED__

/* interface _IDTExtensibility2 */
/* [object][oleautomation][dual][uuid] */ 


EXTERN_C const IID IID__IDTExtensibility2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B65AD801-ABAF-11D0-BB8B-00A0C90F2744")
    _IDTExtensibility2 : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE OnConnection( 
            /* [in] */ IDispatch *Application,
            /* [in] */ ext_ConnectMode ConnectMode,
            /* [in] */ IDispatch *AddInInst,
            /* [in] */ SAFEARRAY * *custom) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE OnDisconnection( 
            /* [in] */ ext_DisconnectMode RemoveMode,
            /* [in] */ SAFEARRAY * *custom) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE OnAddInsUpdate( 
            /* [in] */ SAFEARRAY * *custom) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE OnStartupComplete( 
            /* [in] */ SAFEARRAY * *custom) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE OnBeginShutdown( 
            /* [in] */ SAFEARRAY * *custom) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct _IDTExtensibility2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _IDTExtensibility2 * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _IDTExtensibility2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _IDTExtensibility2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _IDTExtensibility2 * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _IDTExtensibility2 * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _IDTExtensibility2 * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _IDTExtensibility2 * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnConnection )( 
            _IDTExtensibility2 * This,
            /* [in] */ IDispatch *Application,
            /* [in] */ ext_ConnectMode ConnectMode,
            /* [in] */ IDispatch *AddInInst,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnDisconnection )( 
            _IDTExtensibility2 * This,
            /* [in] */ ext_DisconnectMode RemoveMode,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnAddInsUpdate )( 
            _IDTExtensibility2 * This,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnStartupComplete )( 
            _IDTExtensibility2 * This,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnBeginShutdown )( 
            _IDTExtensibility2 * This,
            /* [in] */ SAFEARRAY * *custom);
        
        END_INTERFACE
    } _IDTExtensibility2Vtbl;

    interface _IDTExtensibility2
    {
        CONST_VTBL struct _IDTExtensibility2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IDTExtensibility2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _IDTExtensibility2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _IDTExtensibility2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _IDTExtensibility2_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _IDTExtensibility2_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _IDTExtensibility2_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _IDTExtensibility2_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define _IDTExtensibility2_OnConnection(This,Application,ConnectMode,AddInInst,custom)	\
    ( (This)->lpVtbl -> OnConnection(This,Application,ConnectMode,AddInInst,custom) ) 

#define _IDTExtensibility2_OnDisconnection(This,RemoveMode,custom)	\
    ( (This)->lpVtbl -> OnDisconnection(This,RemoveMode,custom) ) 

#define _IDTExtensibility2_OnAddInsUpdate(This,custom)	\
    ( (This)->lpVtbl -> OnAddInsUpdate(This,custom) ) 

#define _IDTExtensibility2_OnStartupComplete(This,custom)	\
    ( (This)->lpVtbl -> OnStartupComplete(This,custom) ) 

#define _IDTExtensibility2_OnBeginShutdown(This,custom)	\
    ( (This)->lpVtbl -> OnBeginShutdown(This,custom) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* ___IDTExtensibility2_INTERFACE_DEFINED__ */


#ifndef __IRibbonExtensibility_INTERFACE_DEFINED__
#define __IRibbonExtensibility_INTERFACE_DEFINED__

/* interface IRibbonExtensibility */
/* [object][oleautomation][dual][helpcontext][uuid] */ 


EXTERN_C const IID IID_IRibbonExtensibility;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("000C0396-0000-0000-C000-000000000046")
    IRibbonExtensibility : public IDispatch
    {
    public:
        virtual /* [helpcontext][id] */ HRESULT STDMETHODCALLTYPE GetCustomUI( 
            /* [in] */ BSTR RibbonID,
            /* [retval][out] */ BSTR *RibbonXml) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IRibbonExtensibilityVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IRibbonExtensibility * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IRibbonExtensibility * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IRibbonExtensibility * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IRibbonExtensibility * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IRibbonExtensibility * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IRibbonExtensibility * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IRibbonExtensibility * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [helpcontext][id] */ HRESULT ( STDMETHODCALLTYPE *GetCustomUI )( 
            IRibbonExtensibility * This,
            /* [in] */ BSTR RibbonID,
            /* [retval][out] */ BSTR *RibbonXml);
        
        END_INTERFACE
    } IRibbonExtensibilityVtbl;

    interface IRibbonExtensibility
    {
        CONST_VTBL struct IRibbonExtensibilityVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IRibbonExtensibility_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IRibbonExtensibility_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IRibbonExtensibility_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IRibbonExtensibility_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IRibbonExtensibility_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IRibbonExtensibility_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IRibbonExtensibility_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IRibbonExtensibility_GetCustomUI(This,RibbonID,RibbonXml)	\
    ( (This)->lpVtbl -> GetCustomUI(This,RibbonID,RibbonXml) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IRibbonExtensibility_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_LakesideLoungeOffice_0000_0002 */
/* [local] */ 

typedef /* [uuid] */  DECLSPEC_UUID("129E9AF1-4973-11D1-AE81-00A0C90F2600") 
enum reportType
    {
        reportDaily	= 0,
        reportWeekly	= 1
    } 	reportType;



extern RPC_IF_HANDLE __MIDL_itf_LakesideLoungeOffice_0000_0002_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_LakesideLoungeOffice_0000_0002_v0_0_s_ifspec;


#ifndef __LakesideLoungeLib_LIBRARY_DEFINED__
#define __LakesideLoungeLib_LIBRARY_DEFINED__

/* library LakesideLoungeLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_LakesideLoungeLib;

#ifndef __IReport_INTERFACE_DEFINED__
#define __IReport_INTERFACE_DEFINED__

/* interface IReport */
/* [oleautomation][dual][uuid][object] */ 


EXTERN_C const IID IID_IReport;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FAEB4A12-ADF8-447e-BA0C-01709ABC7AC2")
    IReport : public IDispatch
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Init( 
            /* [in] */ IDispatch *arg1,
            /* [in] */ enum reportType repType,
            /* [retval][out] */ VARIANT *arg2) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IReportVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IReport * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IReport * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IReport * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IReport * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IReport * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IReport * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IReport * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        HRESULT ( STDMETHODCALLTYPE *Init )( 
            IReport * This,
            /* [in] */ IDispatch *arg1,
            /* [in] */ enum reportType repType,
            /* [retval][out] */ VARIANT *arg2);
        
        END_INTERFACE
    } IReportVtbl;

    interface IReport
    {
        CONST_VTBL struct IReportVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IReport_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IReport_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IReport_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IReport_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IReport_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IReport_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IReport_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IReport_Init(This,arg1,repType,arg2)	\
    ( (This)->lpVtbl -> Init(This,arg1,repType,arg2) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IReport_INTERFACE_DEFINED__ */


#ifndef __IAddin_INTERFACE_DEFINED__
#define __IAddin_INTERFACE_DEFINED__

/* interface IAddin */
/* [object][uuid] */ 


EXTERN_C const IID IID_IAddin;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("07CA24CF-2779-4c4c-8D3B-D73DD28152FD")
    IAddin : public _IDTExtensibility2
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IAddinVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IAddin * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IAddin * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IAddin * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IAddin * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IAddin * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IAddin * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IAddin * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnConnection )( 
            IAddin * This,
            /* [in] */ IDispatch *Application,
            /* [in] */ ext_ConnectMode ConnectMode,
            /* [in] */ IDispatch *AddInInst,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnDisconnection )( 
            IAddin * This,
            /* [in] */ ext_DisconnectMode RemoveMode,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnAddInsUpdate )( 
            IAddin * This,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnStartupComplete )( 
            IAddin * This,
            /* [in] */ SAFEARRAY * *custom);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnBeginShutdown )( 
            IAddin * This,
            /* [in] */ SAFEARRAY * *custom);
        
        END_INTERFACE
    } IAddinVtbl;

    interface IAddin
    {
        CONST_VTBL struct IAddinVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IAddin_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IAddin_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IAddin_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IAddin_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IAddin_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IAddin_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IAddin_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IAddin_OnConnection(This,Application,ConnectMode,AddInInst,custom)	\
    ( (This)->lpVtbl -> OnConnection(This,Application,ConnectMode,AddInInst,custom) ) 

#define IAddin_OnDisconnection(This,RemoveMode,custom)	\
    ( (This)->lpVtbl -> OnDisconnection(This,RemoveMode,custom) ) 

#define IAddin_OnAddInsUpdate(This,custom)	\
    ( (This)->lpVtbl -> OnAddInsUpdate(This,custom) ) 

#define IAddin_OnStartupComplete(This,custom)	\
    ( (This)->lpVtbl -> OnStartupComplete(This,custom) ) 

#define IAddin_OnBeginShutdown(This,custom)	\
    ( (This)->lpVtbl -> OnBeginShutdown(This,custom) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IAddin_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_Report;

#ifdef __cplusplus

class DECLSPEC_UUID("E30E5FB4-0111-4198-90B6-8EE44CDCCD75")
Report;
#endif

EXTERN_C const CLSID CLSID_Addin;

#ifdef __cplusplus

class DECLSPEC_UUID("0D2CCDFD-51D3-4882-A88C-164CCE65EB38")
Addin;
#endif
#endif /* __LakesideLoungeLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

unsigned long             __RPC_USER  LPSAFEARRAY_UserSize(     unsigned long *, unsigned long            , LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserMarshal(  unsigned long *, unsigned char *, LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserUnmarshal(unsigned long *, unsigned char *, LPSAFEARRAY * ); 
void                      __RPC_USER  LPSAFEARRAY_UserFree(     unsigned long *, LPSAFEARRAY * ); 

unsigned long             __RPC_USER  BSTR_UserSize64(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal64(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal64(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree64(     unsigned long *, BSTR * ); 

unsigned long             __RPC_USER  LPSAFEARRAY_UserSize64(     unsigned long *, unsigned long            , LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserMarshal64(  unsigned long *, unsigned char *, LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserUnmarshal64(unsigned long *, unsigned char *, LPSAFEARRAY * ); 
void                      __RPC_USER  LPSAFEARRAY_UserFree64(     unsigned long *, LPSAFEARRAY * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


