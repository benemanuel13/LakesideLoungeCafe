

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 03:14:07 2038
 */
/* Compiler settings for Various.idl:
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


#ifndef __Various_h_h__
#define __Various_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

/* header files for imported files */
#include "oaidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_Various_0000_0000 */
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



extern RPC_IF_HANDLE __MIDL_itf_Various_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Various_0000_0000_v0_0_s_ifspec;

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


