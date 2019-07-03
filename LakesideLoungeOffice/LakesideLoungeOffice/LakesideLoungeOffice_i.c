

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


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



#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        EXTERN_C __declspec(selectany) const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif // !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, IID__IDTExtensibility2,0xB65AD801,0xABAF,0x11D0,0xBB,0x8B,0x00,0xA0,0xC9,0x0F,0x27,0x44);


MIDL_DEFINE_GUID(IID, IID_IRibbonExtensibility,0x000C0396,0x0000,0x0000,0xC0,0x00,0x00,0x00,0x00,0x00,0x00,0x46);


MIDL_DEFINE_GUID(IID, LIBID_LakesideLoungeLib,0x4C945CD7,0x3805,0x4153,0x8B,0xBE,0xE1,0x7D,0xBB,0xD2,0xE5,0x7C);


MIDL_DEFINE_GUID(IID, IID_IReport,0xFAEB4A12,0xADF8,0x447e,0xBA,0x0C,0x01,0x70,0x9A,0xBC,0x7A,0xC2);


MIDL_DEFINE_GUID(IID, IID_IAddin,0x07CA24CF,0x2779,0x4c4c,0x8D,0x3B,0xD7,0x3D,0xD2,0x81,0x52,0xFD);


MIDL_DEFINE_GUID(CLSID, CLSID_Report,0xE30E5FB4,0x0111,0x4198,0x90,0xB6,0x8E,0xE4,0x4C,0xDC,0xCD,0x75);


MIDL_DEFINE_GUID(CLSID, CLSID_Addin,0x0D2CCDFD,0x51D3,0x4882,0xA8,0x8C,0x16,0x4C,0xCE,0x65,0xEB,0x38);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



