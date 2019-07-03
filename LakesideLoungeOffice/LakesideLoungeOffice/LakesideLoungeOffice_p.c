

/* this ALWAYS GENERATED file contains the proxy stub code */


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

#if !defined(_M_IA64) && !defined(_M_AMD64) && !defined(_ARM_)


#if _MSC_VER >= 1200
#pragma warning(push)
#endif

#pragma warning( disable: 4211 )  /* redefine extern to static */
#pragma warning( disable: 4232 )  /* dllimport identity*/
#pragma warning( disable: 4024 )  /* array to pointer mapping*/
#pragma warning( disable: 4152 )  /* function/data pointer conversion in expression */
#pragma warning( disable: 4100 ) /* unreferenced arguments in x86 call */

#pragma optimize("", off ) 

#define USE_STUBLESS_PROXY


/* verify that the <rpcproxy.h> version is high enough to compile this file*/
#ifndef __REDQ_RPCPROXY_H_VERSION__
#define __REQUIRED_RPCPROXY_H_VERSION__ 475
#endif


#include "rpcproxy.h"
#ifndef __RPCPROXY_H_VERSION__
#error this stub requires an updated version of <rpcproxy.h>
#endif /* __RPCPROXY_H_VERSION__ */


#include "LakesideLoungeOffice_h.h"

#define TYPE_FORMAT_STRING_SIZE   1229                              
#define PROC_FORMAT_STRING_SIZE   247                               
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   2            

typedef struct _LakesideLoungeOffice_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } LakesideLoungeOffice_MIDL_TYPE_FORMAT_STRING;

typedef struct _LakesideLoungeOffice_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } LakesideLoungeOffice_MIDL_PROC_FORMAT_STRING;

typedef struct _LakesideLoungeOffice_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } LakesideLoungeOffice_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};


extern const LakesideLoungeOffice_MIDL_TYPE_FORMAT_STRING LakesideLoungeOffice__MIDL_TypeFormatString;
extern const LakesideLoungeOffice_MIDL_PROC_FORMAT_STRING LakesideLoungeOffice__MIDL_ProcFormatString;
extern const LakesideLoungeOffice_MIDL_EXPR_FORMAT_STRING LakesideLoungeOffice__MIDL_ExprFormatString;


extern const MIDL_STUB_DESC Object_StubDesc;


extern const MIDL_SERVER_INFO _IDTExtensibility2_ServerInfo;
extern const MIDL_STUBLESS_PROXY_INFO _IDTExtensibility2_ProxyInfo;


extern const MIDL_STUB_DESC Object_StubDesc;


extern const MIDL_SERVER_INFO IRibbonExtensibility_ServerInfo;
extern const MIDL_STUBLESS_PROXY_INFO IRibbonExtensibility_ProxyInfo;


extern const USER_MARSHAL_ROUTINE_QUADRUPLE UserMarshalRoutines[ WIRE_MARSHAL_TABLE_SIZE ];

#if !defined(__RPC_WIN32__)
#error  Invalid build platform for this stub.
#endif
#if !(TARGET_IS_NT60_OR_LATER)
#error You need Windows Vista or later to run this stub because it uses these features:
#error   forced complex structure or array, new range semantics, compiled for Windows Vista.
#error However, your C/C++ compilation flags indicate you intend to run this app on earlier systems.
#error This app will fail with the RPC_X_WRONG_STUB_VERSION error.
#endif


static const LakesideLoungeOffice_MIDL_PROC_FORMAT_STRING LakesideLoungeOffice__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure OnConnection */

			0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x7 ),	/* 7 */
/*  8 */	NdrFcShort( 0x18 ),	/* x86 Stack size/offset = 24 */
/* 10 */	NdrFcShort( 0x6 ),	/* 6 */
/* 12 */	NdrFcShort( 0x8 ),	/* 8 */
/* 14 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x5,		/* 5 */
/* 16 */	0x8,		/* 8 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 18 */	NdrFcShort( 0x0 ),	/* 0 */
/* 20 */	NdrFcShort( 0x1 ),	/* 1 */
/* 22 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter Application */

/* 24 */	NdrFcShort( 0xb ),	/* Flags:  must size, must free, in, */
/* 26 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 28 */	NdrFcShort( 0x2 ),	/* Type Offset=2 */

	/* Parameter ConnectMode */

/* 30 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 32 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 34 */	0xd,		/* FC_ENUM16 */
			0x0,		/* 0 */

	/* Parameter AddInInst */

/* 36 */	NdrFcShort( 0xb ),	/* Flags:  must size, must free, in, */
/* 38 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 40 */	NdrFcShort( 0x2 ),	/* Type Offset=2 */

	/* Parameter custom */

/* 42 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 44 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 46 */	NdrFcShort( 0x4a6 ),	/* Type Offset=1190 */

	/* Return value */

/* 48 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 50 */	NdrFcShort( 0x14 ),	/* x86 Stack size/offset = 20 */
/* 52 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure OnDisconnection */

/* 54 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 56 */	NdrFcLong( 0x0 ),	/* 0 */
/* 60 */	NdrFcShort( 0x8 ),	/* 8 */
/* 62 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 64 */	NdrFcShort( 0x6 ),	/* 6 */
/* 66 */	NdrFcShort( 0x8 ),	/* 8 */
/* 68 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x3,		/* 3 */
/* 70 */	0x8,		/* 8 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 72 */	NdrFcShort( 0x0 ),	/* 0 */
/* 74 */	NdrFcShort( 0x1 ),	/* 1 */
/* 76 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter RemoveMode */

/* 78 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 80 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 82 */	0xd,		/* FC_ENUM16 */
			0x0,		/* 0 */

	/* Parameter custom */

/* 84 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 86 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 88 */	NdrFcShort( 0x4a6 ),	/* Type Offset=1190 */

	/* Return value */

/* 90 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 92 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 94 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure OnAddInsUpdate */

/* 96 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 98 */	NdrFcLong( 0x0 ),	/* 0 */
/* 102 */	NdrFcShort( 0x9 ),	/* 9 */
/* 104 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 106 */	NdrFcShort( 0x0 ),	/* 0 */
/* 108 */	NdrFcShort( 0x8 ),	/* 8 */
/* 110 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x2,		/* 2 */
/* 112 */	0x8,		/* 8 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 114 */	NdrFcShort( 0x0 ),	/* 0 */
/* 116 */	NdrFcShort( 0x1 ),	/* 1 */
/* 118 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter custom */

/* 120 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 122 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 124 */	NdrFcShort( 0x4a6 ),	/* Type Offset=1190 */

	/* Return value */

/* 126 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 128 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 130 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure OnStartupComplete */

/* 132 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 134 */	NdrFcLong( 0x0 ),	/* 0 */
/* 138 */	NdrFcShort( 0xa ),	/* 10 */
/* 140 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 142 */	NdrFcShort( 0x0 ),	/* 0 */
/* 144 */	NdrFcShort( 0x8 ),	/* 8 */
/* 146 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x2,		/* 2 */
/* 148 */	0x8,		/* 8 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 150 */	NdrFcShort( 0x0 ),	/* 0 */
/* 152 */	NdrFcShort( 0x1 ),	/* 1 */
/* 154 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter custom */

/* 156 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 158 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 160 */	NdrFcShort( 0x4a6 ),	/* Type Offset=1190 */

	/* Return value */

/* 162 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 164 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 166 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure OnBeginShutdown */

/* 168 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 170 */	NdrFcLong( 0x0 ),	/* 0 */
/* 174 */	NdrFcShort( 0xb ),	/* 11 */
/* 176 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 178 */	NdrFcShort( 0x0 ),	/* 0 */
/* 180 */	NdrFcShort( 0x8 ),	/* 8 */
/* 182 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x2,		/* 2 */
/* 184 */	0x8,		/* 8 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 186 */	NdrFcShort( 0x0 ),	/* 0 */
/* 188 */	NdrFcShort( 0x1 ),	/* 1 */
/* 190 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter custom */

/* 192 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 194 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 196 */	NdrFcShort( 0x4a6 ),	/* Type Offset=1190 */

	/* Return value */

/* 198 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 200 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 202 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure GetCustomUI */

/* 204 */	0x33,		/* FC_AUTO_HANDLE */
			0x6c,		/* Old Flags:  object, Oi2 */
/* 206 */	NdrFcLong( 0x0 ),	/* 0 */
/* 210 */	NdrFcShort( 0x7 ),	/* 7 */
/* 212 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 214 */	NdrFcShort( 0x0 ),	/* 0 */
/* 216 */	NdrFcShort( 0x8 ),	/* 8 */
/* 218 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x3,		/* 3 */
/* 220 */	0x8,		/* 8 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 222 */	NdrFcShort( 0x1 ),	/* 1 */
/* 224 */	NdrFcShort( 0x1 ),	/* 1 */
/* 226 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter RibbonID */

/* 228 */	NdrFcShort( 0x8b ),	/* Flags:  must size, must free, in, by val, */
/* 230 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 232 */	NdrFcShort( 0x4b0 ),	/* Type Offset=1200 */

	/* Parameter RibbonXml */

/* 234 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 236 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 238 */	NdrFcShort( 0x4c2 ),	/* Type Offset=1218 */

	/* Return value */

/* 240 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 242 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 244 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

			0x0
        }
    };

static const LakesideLoungeOffice_MIDL_TYPE_FORMAT_STRING LakesideLoungeOffice__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */
/*  2 */	
			0x2f,		/* FC_IP */
			0x5a,		/* FC_CONSTANT_IID */
/*  4 */	NdrFcLong( 0x20400 ),	/* 132096 */
/*  8 */	NdrFcShort( 0x0 ),	/* 0 */
/* 10 */	NdrFcShort( 0x0 ),	/* 0 */
/* 12 */	0xc0,		/* 192 */
			0x0,		/* 0 */
/* 14 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 16 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 18 */	0x0,		/* 0 */
			0x46,		/* 70 */
/* 20 */	
			0x11, 0x0,	/* FC_RP */
/* 22 */	NdrFcShort( 0x490 ),	/* Offset= 1168 (1190) */
/* 24 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 26 */	NdrFcShort( 0x2 ),	/* Offset= 2 (28) */
/* 28 */	
			0x12, 0x0,	/* FC_UP */
/* 30 */	NdrFcShort( 0x476 ),	/* Offset= 1142 (1172) */
/* 32 */	
			0x2a,		/* FC_ENCAPSULATED_UNION */
			0x49,		/* 73 */
/* 34 */	NdrFcShort( 0x18 ),	/* 24 */
/* 36 */	NdrFcShort( 0xa ),	/* 10 */
/* 38 */	NdrFcLong( 0x8 ),	/* 8 */
/* 42 */	NdrFcShort( 0x84 ),	/* Offset= 132 (174) */
/* 44 */	NdrFcLong( 0xd ),	/* 13 */
/* 48 */	NdrFcShort( 0xce ),	/* Offset= 206 (254) */
/* 50 */	NdrFcLong( 0x9 ),	/* 9 */
/* 54 */	NdrFcShort( 0x102 ),	/* Offset= 258 (312) */
/* 56 */	NdrFcLong( 0xc ),	/* 12 */
/* 60 */	NdrFcShort( 0x31e ),	/* Offset= 798 (858) */
/* 62 */	NdrFcLong( 0x24 ),	/* 36 */
/* 66 */	NdrFcShort( 0x352 ),	/* Offset= 850 (916) */
/* 68 */	NdrFcLong( 0x800d ),	/* 32781 */
/* 72 */	NdrFcShort( 0x36e ),	/* Offset= 878 (950) */
/* 74 */	NdrFcLong( 0x10 ),	/* 16 */
/* 78 */	NdrFcShort( 0x392 ),	/* Offset= 914 (992) */
/* 80 */	NdrFcLong( 0x2 ),	/* 2 */
/* 84 */	NdrFcShort( 0x3b6 ),	/* Offset= 950 (1034) */
/* 86 */	NdrFcLong( 0x3 ),	/* 3 */
/* 90 */	NdrFcShort( 0x3da ),	/* Offset= 986 (1076) */
/* 92 */	NdrFcLong( 0x14 ),	/* 20 */
/* 96 */	NdrFcShort( 0x3fe ),	/* Offset= 1022 (1118) */
/* 98 */	NdrFcShort( 0xffff ),	/* Offset= -1 (97) */
/* 100 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 102 */	NdrFcShort( 0x2 ),	/* 2 */
/* 104 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 106 */	NdrFcShort( 0xfffc ),	/* -4 */
/* 108 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 110 */	0x0 , 
			0x0,		/* 0 */
/* 112 */	NdrFcLong( 0x0 ),	/* 0 */
/* 116 */	NdrFcLong( 0x0 ),	/* 0 */
/* 120 */	0x6,		/* FC_SHORT */
			0x5b,		/* FC_END */
/* 122 */	
			0x17,		/* FC_CSTRUCT */
			0x3,		/* 3 */
/* 124 */	NdrFcShort( 0x8 ),	/* 8 */
/* 126 */	NdrFcShort( 0xffe6 ),	/* Offset= -26 (100) */
/* 128 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 130 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 132 */	
			0x1b,		/* FC_CARRAY */
			0x3,		/* 3 */
/* 134 */	NdrFcShort( 0x4 ),	/* 4 */
/* 136 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 138 */	NdrFcShort( 0x0 ),	/* 0 */
/* 140 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 142 */	0x0 , 
			0x0,		/* 0 */
/* 144 */	NdrFcLong( 0x0 ),	/* 0 */
/* 148 */	NdrFcLong( 0x0 ),	/* 0 */
/* 152 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 154 */	
			0x48,		/* FC_VARIABLE_REPEAT */
			0x49,		/* FC_FIXED_OFFSET */
/* 156 */	NdrFcShort( 0x4 ),	/* 4 */
/* 158 */	NdrFcShort( 0x0 ),	/* 0 */
/* 160 */	NdrFcShort( 0x1 ),	/* 1 */
/* 162 */	NdrFcShort( 0x0 ),	/* 0 */
/* 164 */	NdrFcShort( 0x0 ),	/* 0 */
/* 166 */	0x12, 0x0,	/* FC_UP */
/* 168 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (122) */
/* 170 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 172 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 174 */	
			0x16,		/* FC_PSTRUCT */
			0x3,		/* 3 */
/* 176 */	NdrFcShort( 0x8 ),	/* 8 */
/* 178 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 180 */	
			0x46,		/* FC_NO_REPEAT */
			0x5c,		/* FC_PAD */
/* 182 */	NdrFcShort( 0x4 ),	/* 4 */
/* 184 */	NdrFcShort( 0x4 ),	/* 4 */
/* 186 */	0x11, 0x0,	/* FC_RP */
/* 188 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (132) */
/* 190 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 192 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 194 */	
			0x2f,		/* FC_IP */
			0x5a,		/* FC_CONSTANT_IID */
/* 196 */	NdrFcLong( 0x0 ),	/* 0 */
/* 200 */	NdrFcShort( 0x0 ),	/* 0 */
/* 202 */	NdrFcShort( 0x0 ),	/* 0 */
/* 204 */	0xc0,		/* 192 */
			0x0,		/* 0 */
/* 206 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 208 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 210 */	0x0,		/* 0 */
			0x46,		/* 70 */
/* 212 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 214 */	NdrFcShort( 0x0 ),	/* 0 */
/* 216 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 218 */	NdrFcShort( 0x0 ),	/* 0 */
/* 220 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 222 */	0x0 , 
			0x0,		/* 0 */
/* 224 */	NdrFcLong( 0x0 ),	/* 0 */
/* 228 */	NdrFcLong( 0x0 ),	/* 0 */
/* 232 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 236 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 238 */	0x0 , 
			0x0,		/* 0 */
/* 240 */	NdrFcLong( 0x0 ),	/* 0 */
/* 244 */	NdrFcLong( 0x0 ),	/* 0 */
/* 248 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 250 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (194) */
/* 252 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 254 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 256 */	NdrFcShort( 0x8 ),	/* 8 */
/* 258 */	NdrFcShort( 0x0 ),	/* 0 */
/* 260 */	NdrFcShort( 0x6 ),	/* Offset= 6 (266) */
/* 262 */	0x8,		/* FC_LONG */
			0x36,		/* FC_POINTER */
/* 264 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 266 */	
			0x11, 0x0,	/* FC_RP */
/* 268 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (212) */
/* 270 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 272 */	NdrFcShort( 0x0 ),	/* 0 */
/* 274 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 276 */	NdrFcShort( 0x0 ),	/* 0 */
/* 278 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 280 */	0x0 , 
			0x0,		/* 0 */
/* 282 */	NdrFcLong( 0x0 ),	/* 0 */
/* 286 */	NdrFcLong( 0x0 ),	/* 0 */
/* 290 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 294 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 296 */	0x0 , 
			0x0,		/* 0 */
/* 298 */	NdrFcLong( 0x0 ),	/* 0 */
/* 302 */	NdrFcLong( 0x0 ),	/* 0 */
/* 306 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 308 */	NdrFcShort( 0xfece ),	/* Offset= -306 (2) */
/* 310 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 312 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 314 */	NdrFcShort( 0x8 ),	/* 8 */
/* 316 */	NdrFcShort( 0x0 ),	/* 0 */
/* 318 */	NdrFcShort( 0x6 ),	/* Offset= 6 (324) */
/* 320 */	0x8,		/* FC_LONG */
			0x36,		/* FC_POINTER */
/* 322 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 324 */	
			0x11, 0x0,	/* FC_RP */
/* 326 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (270) */
/* 328 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 330 */	0x7,		/* Corr desc: FC_USHORT */
			0x0,		/*  */
/* 332 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 334 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 336 */	0x0 , 
			0x0,		/* 0 */
/* 338 */	NdrFcLong( 0x0 ),	/* 0 */
/* 342 */	NdrFcLong( 0x0 ),	/* 0 */
/* 346 */	NdrFcShort( 0x2 ),	/* Offset= 2 (348) */
/* 348 */	NdrFcShort( 0x10 ),	/* 16 */
/* 350 */	NdrFcShort( 0x2f ),	/* 47 */
/* 352 */	NdrFcLong( 0x14 ),	/* 20 */
/* 356 */	NdrFcShort( 0x800b ),	/* Simple arm type: FC_HYPER */
/* 358 */	NdrFcLong( 0x3 ),	/* 3 */
/* 362 */	NdrFcShort( 0x8008 ),	/* Simple arm type: FC_LONG */
/* 364 */	NdrFcLong( 0x11 ),	/* 17 */
/* 368 */	NdrFcShort( 0x8001 ),	/* Simple arm type: FC_BYTE */
/* 370 */	NdrFcLong( 0x2 ),	/* 2 */
/* 374 */	NdrFcShort( 0x8006 ),	/* Simple arm type: FC_SHORT */
/* 376 */	NdrFcLong( 0x4 ),	/* 4 */
/* 380 */	NdrFcShort( 0x800a ),	/* Simple arm type: FC_FLOAT */
/* 382 */	NdrFcLong( 0x5 ),	/* 5 */
/* 386 */	NdrFcShort( 0x800c ),	/* Simple arm type: FC_DOUBLE */
/* 388 */	NdrFcLong( 0xb ),	/* 11 */
/* 392 */	NdrFcShort( 0x8006 ),	/* Simple arm type: FC_SHORT */
/* 394 */	NdrFcLong( 0xa ),	/* 10 */
/* 398 */	NdrFcShort( 0x8008 ),	/* Simple arm type: FC_LONG */
/* 400 */	NdrFcLong( 0x6 ),	/* 6 */
/* 404 */	NdrFcShort( 0xe8 ),	/* Offset= 232 (636) */
/* 406 */	NdrFcLong( 0x7 ),	/* 7 */
/* 410 */	NdrFcShort( 0x800c ),	/* Simple arm type: FC_DOUBLE */
/* 412 */	NdrFcLong( 0x8 ),	/* 8 */
/* 416 */	NdrFcShort( 0xe2 ),	/* Offset= 226 (642) */
/* 418 */	NdrFcLong( 0xd ),	/* 13 */
/* 422 */	NdrFcShort( 0xff1c ),	/* Offset= -228 (194) */
/* 424 */	NdrFcLong( 0x9 ),	/* 9 */
/* 428 */	NdrFcShort( 0xfe56 ),	/* Offset= -426 (2) */
/* 430 */	NdrFcLong( 0x2000 ),	/* 8192 */
/* 434 */	NdrFcShort( 0xd4 ),	/* Offset= 212 (646) */
/* 436 */	NdrFcLong( 0x24 ),	/* 36 */
/* 440 */	NdrFcShort( 0xd6 ),	/* Offset= 214 (654) */
/* 442 */	NdrFcLong( 0x4024 ),	/* 16420 */
/* 446 */	NdrFcShort( 0xd0 ),	/* Offset= 208 (654) */
/* 448 */	NdrFcLong( 0x4011 ),	/* 16401 */
/* 452 */	NdrFcShort( 0x10a ),	/* Offset= 266 (718) */
/* 454 */	NdrFcLong( 0x4002 ),	/* 16386 */
/* 458 */	NdrFcShort( 0x108 ),	/* Offset= 264 (722) */
/* 460 */	NdrFcLong( 0x4003 ),	/* 16387 */
/* 464 */	NdrFcShort( 0x106 ),	/* Offset= 262 (726) */
/* 466 */	NdrFcLong( 0x4014 ),	/* 16404 */
/* 470 */	NdrFcShort( 0x104 ),	/* Offset= 260 (730) */
/* 472 */	NdrFcLong( 0x4004 ),	/* 16388 */
/* 476 */	NdrFcShort( 0x102 ),	/* Offset= 258 (734) */
/* 478 */	NdrFcLong( 0x4005 ),	/* 16389 */
/* 482 */	NdrFcShort( 0x100 ),	/* Offset= 256 (738) */
/* 484 */	NdrFcLong( 0x400b ),	/* 16395 */
/* 488 */	NdrFcShort( 0xea ),	/* Offset= 234 (722) */
/* 490 */	NdrFcLong( 0x400a ),	/* 16394 */
/* 494 */	NdrFcShort( 0xe8 ),	/* Offset= 232 (726) */
/* 496 */	NdrFcLong( 0x4006 ),	/* 16390 */
/* 500 */	NdrFcShort( 0xf2 ),	/* Offset= 242 (742) */
/* 502 */	NdrFcLong( 0x4007 ),	/* 16391 */
/* 506 */	NdrFcShort( 0xe8 ),	/* Offset= 232 (738) */
/* 508 */	NdrFcLong( 0x4008 ),	/* 16392 */
/* 512 */	NdrFcShort( 0xea ),	/* Offset= 234 (746) */
/* 514 */	NdrFcLong( 0x400d ),	/* 16397 */
/* 518 */	NdrFcShort( 0xe8 ),	/* Offset= 232 (750) */
/* 520 */	NdrFcLong( 0x4009 ),	/* 16393 */
/* 524 */	NdrFcShort( 0xe6 ),	/* Offset= 230 (754) */
/* 526 */	NdrFcLong( 0x6000 ),	/* 24576 */
/* 530 */	NdrFcShort( 0xe4 ),	/* Offset= 228 (758) */
/* 532 */	NdrFcLong( 0x400c ),	/* 16396 */
/* 536 */	NdrFcShort( 0xea ),	/* Offset= 234 (770) */
/* 538 */	NdrFcLong( 0x10 ),	/* 16 */
/* 542 */	NdrFcShort( 0x8002 ),	/* Simple arm type: FC_CHAR */
/* 544 */	NdrFcLong( 0x12 ),	/* 18 */
/* 548 */	NdrFcShort( 0x8006 ),	/* Simple arm type: FC_SHORT */
/* 550 */	NdrFcLong( 0x13 ),	/* 19 */
/* 554 */	NdrFcShort( 0x8008 ),	/* Simple arm type: FC_LONG */
/* 556 */	NdrFcLong( 0x15 ),	/* 21 */
/* 560 */	NdrFcShort( 0x800b ),	/* Simple arm type: FC_HYPER */
/* 562 */	NdrFcLong( 0x16 ),	/* 22 */
/* 566 */	NdrFcShort( 0x8008 ),	/* Simple arm type: FC_LONG */
/* 568 */	NdrFcLong( 0x17 ),	/* 23 */
/* 572 */	NdrFcShort( 0x8008 ),	/* Simple arm type: FC_LONG */
/* 574 */	NdrFcLong( 0xe ),	/* 14 */
/* 578 */	NdrFcShort( 0xc8 ),	/* Offset= 200 (778) */
/* 580 */	NdrFcLong( 0x400e ),	/* 16398 */
/* 584 */	NdrFcShort( 0xcc ),	/* Offset= 204 (788) */
/* 586 */	NdrFcLong( 0x4010 ),	/* 16400 */
/* 590 */	NdrFcShort( 0xca ),	/* Offset= 202 (792) */
/* 592 */	NdrFcLong( 0x4012 ),	/* 16402 */
/* 596 */	NdrFcShort( 0x7e ),	/* Offset= 126 (722) */
/* 598 */	NdrFcLong( 0x4013 ),	/* 16403 */
/* 602 */	NdrFcShort( 0x7c ),	/* Offset= 124 (726) */
/* 604 */	NdrFcLong( 0x4015 ),	/* 16405 */
/* 608 */	NdrFcShort( 0x7a ),	/* Offset= 122 (730) */
/* 610 */	NdrFcLong( 0x4016 ),	/* 16406 */
/* 614 */	NdrFcShort( 0x70 ),	/* Offset= 112 (726) */
/* 616 */	NdrFcLong( 0x4017 ),	/* 16407 */
/* 620 */	NdrFcShort( 0x6a ),	/* Offset= 106 (726) */
/* 622 */	NdrFcLong( 0x0 ),	/* 0 */
/* 626 */	NdrFcShort( 0x0 ),	/* Offset= 0 (626) */
/* 628 */	NdrFcLong( 0x1 ),	/* 1 */
/* 632 */	NdrFcShort( 0x0 ),	/* Offset= 0 (632) */
/* 634 */	NdrFcShort( 0xffff ),	/* Offset= -1 (633) */
/* 636 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 638 */	NdrFcShort( 0x8 ),	/* 8 */
/* 640 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 642 */	
			0x12, 0x0,	/* FC_UP */
/* 644 */	NdrFcShort( 0xfdf6 ),	/* Offset= -522 (122) */
/* 646 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 648 */	NdrFcShort( 0x2 ),	/* Offset= 2 (650) */
/* 650 */	
			0x12, 0x0,	/* FC_UP */
/* 652 */	NdrFcShort( 0x208 ),	/* Offset= 520 (1172) */
/* 654 */	
			0x12, 0x0,	/* FC_UP */
/* 656 */	NdrFcShort( 0x2a ),	/* Offset= 42 (698) */
/* 658 */	
			0x2f,		/* FC_IP */
			0x5a,		/* FC_CONSTANT_IID */
/* 660 */	NdrFcLong( 0x2f ),	/* 47 */
/* 664 */	NdrFcShort( 0x0 ),	/* 0 */
/* 666 */	NdrFcShort( 0x0 ),	/* 0 */
/* 668 */	0xc0,		/* 192 */
			0x0,		/* 0 */
/* 670 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 672 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 674 */	0x0,		/* 0 */
			0x46,		/* 70 */
/* 676 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 678 */	NdrFcShort( 0x1 ),	/* 1 */
/* 680 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 682 */	NdrFcShort( 0x4 ),	/* 4 */
/* 684 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 686 */	0x0 , 
			0x0,		/* 0 */
/* 688 */	NdrFcLong( 0x0 ),	/* 0 */
/* 692 */	NdrFcLong( 0x0 ),	/* 0 */
/* 696 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 698 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 700 */	NdrFcShort( 0x10 ),	/* 16 */
/* 702 */	NdrFcShort( 0x0 ),	/* 0 */
/* 704 */	NdrFcShort( 0xa ),	/* Offset= 10 (714) */
/* 706 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 708 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 710 */	NdrFcShort( 0xffcc ),	/* Offset= -52 (658) */
/* 712 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 714 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 716 */	NdrFcShort( 0xffd8 ),	/* Offset= -40 (676) */
/* 718 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 720 */	0x1,		/* FC_BYTE */
			0x5c,		/* FC_PAD */
/* 722 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 724 */	0x6,		/* FC_SHORT */
			0x5c,		/* FC_PAD */
/* 726 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 728 */	0x8,		/* FC_LONG */
			0x5c,		/* FC_PAD */
/* 730 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 732 */	0xb,		/* FC_HYPER */
			0x5c,		/* FC_PAD */
/* 734 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 736 */	0xa,		/* FC_FLOAT */
			0x5c,		/* FC_PAD */
/* 738 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 740 */	0xc,		/* FC_DOUBLE */
			0x5c,		/* FC_PAD */
/* 742 */	
			0x12, 0x0,	/* FC_UP */
/* 744 */	NdrFcShort( 0xff94 ),	/* Offset= -108 (636) */
/* 746 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 748 */	NdrFcShort( 0xff96 ),	/* Offset= -106 (642) */
/* 750 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 752 */	NdrFcShort( 0xfdd2 ),	/* Offset= -558 (194) */
/* 754 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 756 */	NdrFcShort( 0xfd0e ),	/* Offset= -754 (2) */
/* 758 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 760 */	NdrFcShort( 0x2 ),	/* Offset= 2 (762) */
/* 762 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 764 */	NdrFcShort( 0x2 ),	/* Offset= 2 (766) */
/* 766 */	
			0x12, 0x0,	/* FC_UP */
/* 768 */	NdrFcShort( 0x194 ),	/* Offset= 404 (1172) */
/* 770 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 772 */	NdrFcShort( 0x2 ),	/* Offset= 2 (774) */
/* 774 */	
			0x12, 0x0,	/* FC_UP */
/* 776 */	NdrFcShort( 0x14 ),	/* Offset= 20 (796) */
/* 778 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 780 */	NdrFcShort( 0x10 ),	/* 16 */
/* 782 */	0x6,		/* FC_SHORT */
			0x1,		/* FC_BYTE */
/* 784 */	0x1,		/* FC_BYTE */
			0x8,		/* FC_LONG */
/* 786 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 788 */	
			0x12, 0x0,	/* FC_UP */
/* 790 */	NdrFcShort( 0xfff4 ),	/* Offset= -12 (778) */
/* 792 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 794 */	0x2,		/* FC_CHAR */
			0x5c,		/* FC_PAD */
/* 796 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 798 */	NdrFcShort( 0x20 ),	/* 32 */
/* 800 */	NdrFcShort( 0x0 ),	/* 0 */
/* 802 */	NdrFcShort( 0x0 ),	/* Offset= 0 (802) */
/* 804 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 806 */	0x6,		/* FC_SHORT */
			0x6,		/* FC_SHORT */
/* 808 */	0x6,		/* FC_SHORT */
			0x6,		/* FC_SHORT */
/* 810 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 812 */	NdrFcShort( 0xfe1c ),	/* Offset= -484 (328) */
/* 814 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 816 */	
			0x1b,		/* FC_CARRAY */
			0x3,		/* 3 */
/* 818 */	NdrFcShort( 0x4 ),	/* 4 */
/* 820 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 822 */	NdrFcShort( 0x0 ),	/* 0 */
/* 824 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 826 */	0x0 , 
			0x0,		/* 0 */
/* 828 */	NdrFcLong( 0x0 ),	/* 0 */
/* 832 */	NdrFcLong( 0x0 ),	/* 0 */
/* 836 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 838 */	
			0x48,		/* FC_VARIABLE_REPEAT */
			0x49,		/* FC_FIXED_OFFSET */
/* 840 */	NdrFcShort( 0x4 ),	/* 4 */
/* 842 */	NdrFcShort( 0x0 ),	/* 0 */
/* 844 */	NdrFcShort( 0x1 ),	/* 1 */
/* 846 */	NdrFcShort( 0x0 ),	/* 0 */
/* 848 */	NdrFcShort( 0x0 ),	/* 0 */
/* 850 */	0x12, 0x0,	/* FC_UP */
/* 852 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (796) */
/* 854 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 856 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 858 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 860 */	NdrFcShort( 0x8 ),	/* 8 */
/* 862 */	NdrFcShort( 0x0 ),	/* 0 */
/* 864 */	NdrFcShort( 0x6 ),	/* Offset= 6 (870) */
/* 866 */	0x8,		/* FC_LONG */
			0x36,		/* FC_POINTER */
/* 868 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 870 */	
			0x11, 0x0,	/* FC_RP */
/* 872 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (816) */
/* 874 */	
			0x1b,		/* FC_CARRAY */
			0x3,		/* 3 */
/* 876 */	NdrFcShort( 0x4 ),	/* 4 */
/* 878 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 880 */	NdrFcShort( 0x0 ),	/* 0 */
/* 882 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 884 */	0x0 , 
			0x0,		/* 0 */
/* 886 */	NdrFcLong( 0x0 ),	/* 0 */
/* 890 */	NdrFcLong( 0x0 ),	/* 0 */
/* 894 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 896 */	
			0x48,		/* FC_VARIABLE_REPEAT */
			0x49,		/* FC_FIXED_OFFSET */
/* 898 */	NdrFcShort( 0x4 ),	/* 4 */
/* 900 */	NdrFcShort( 0x0 ),	/* 0 */
/* 902 */	NdrFcShort( 0x1 ),	/* 1 */
/* 904 */	NdrFcShort( 0x0 ),	/* 0 */
/* 906 */	NdrFcShort( 0x0 ),	/* 0 */
/* 908 */	0x12, 0x0,	/* FC_UP */
/* 910 */	NdrFcShort( 0xff2c ),	/* Offset= -212 (698) */
/* 912 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 914 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 916 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 918 */	NdrFcShort( 0x8 ),	/* 8 */
/* 920 */	NdrFcShort( 0x0 ),	/* 0 */
/* 922 */	NdrFcShort( 0x6 ),	/* Offset= 6 (928) */
/* 924 */	0x8,		/* FC_LONG */
			0x36,		/* FC_POINTER */
/* 926 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 928 */	
			0x11, 0x0,	/* FC_RP */
/* 930 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (874) */
/* 932 */	
			0x1d,		/* FC_SMFARRAY */
			0x0,		/* 0 */
/* 934 */	NdrFcShort( 0x8 ),	/* 8 */
/* 936 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 938 */	
			0x15,		/* FC_STRUCT */
			0x3,		/* 3 */
/* 940 */	NdrFcShort( 0x10 ),	/* 16 */
/* 942 */	0x8,		/* FC_LONG */
			0x6,		/* FC_SHORT */
/* 944 */	0x6,		/* FC_SHORT */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 946 */	0x0,		/* 0 */
			NdrFcShort( 0xfff1 ),	/* Offset= -15 (932) */
			0x5b,		/* FC_END */
/* 950 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 952 */	NdrFcShort( 0x18 ),	/* 24 */
/* 954 */	NdrFcShort( 0x0 ),	/* 0 */
/* 956 */	NdrFcShort( 0xa ),	/* Offset= 10 (966) */
/* 958 */	0x8,		/* FC_LONG */
			0x36,		/* FC_POINTER */
/* 960 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 962 */	NdrFcShort( 0xffe8 ),	/* Offset= -24 (938) */
/* 964 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 966 */	
			0x11, 0x0,	/* FC_RP */
/* 968 */	NdrFcShort( 0xfd0c ),	/* Offset= -756 (212) */
/* 970 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 972 */	NdrFcShort( 0x1 ),	/* 1 */
/* 974 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 976 */	NdrFcShort( 0x0 ),	/* 0 */
/* 978 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 980 */	0x0 , 
			0x0,		/* 0 */
/* 982 */	NdrFcLong( 0x0 ),	/* 0 */
/* 986 */	NdrFcLong( 0x0 ),	/* 0 */
/* 990 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 992 */	
			0x16,		/* FC_PSTRUCT */
			0x3,		/* 3 */
/* 994 */	NdrFcShort( 0x8 ),	/* 8 */
/* 996 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 998 */	
			0x46,		/* FC_NO_REPEAT */
			0x5c,		/* FC_PAD */
/* 1000 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1002 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1004 */	0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1006 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (970) */
/* 1008 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 1010 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1012 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 1014 */	NdrFcShort( 0x2 ),	/* 2 */
/* 1016 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1018 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1020 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1022 */	0x0 , 
			0x0,		/* 0 */
/* 1024 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1028 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1032 */	0x6,		/* FC_SHORT */
			0x5b,		/* FC_END */
/* 1034 */	
			0x16,		/* FC_PSTRUCT */
			0x3,		/* 3 */
/* 1036 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1038 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 1040 */	
			0x46,		/* FC_NO_REPEAT */
			0x5c,		/* FC_PAD */
/* 1042 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1044 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1046 */	0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1048 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (1012) */
/* 1050 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 1052 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1054 */	
			0x1b,		/* FC_CARRAY */
			0x3,		/* 3 */
/* 1056 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1058 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1060 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1062 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1064 */	0x0 , 
			0x0,		/* 0 */
/* 1066 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1070 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1074 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1076 */	
			0x16,		/* FC_PSTRUCT */
			0x3,		/* 3 */
/* 1078 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1080 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 1082 */	
			0x46,		/* FC_NO_REPEAT */
			0x5c,		/* FC_PAD */
/* 1084 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1086 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1088 */	0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1090 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (1054) */
/* 1092 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 1094 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1096 */	
			0x1b,		/* FC_CARRAY */
			0x7,		/* 7 */
/* 1098 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1100 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1102 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1104 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1106 */	0x0 , 
			0x0,		/* 0 */
/* 1108 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1112 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1116 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 1118 */	
			0x16,		/* FC_PSTRUCT */
			0x3,		/* 3 */
/* 1120 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1122 */	
			0x4b,		/* FC_PP */
			0x5c,		/* FC_PAD */
/* 1124 */	
			0x46,		/* FC_NO_REPEAT */
			0x5c,		/* FC_PAD */
/* 1126 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1128 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1130 */	0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1132 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (1096) */
/* 1134 */	
			0x5b,		/* FC_END */

			0x8,		/* FC_LONG */
/* 1136 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1138 */	
			0x15,		/* FC_STRUCT */
			0x3,		/* 3 */
/* 1140 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1142 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1144 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1146 */	
			0x1b,		/* FC_CARRAY */
			0x3,		/* 3 */
/* 1148 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1150 */	0x7,		/* Corr desc: FC_USHORT */
			0x0,		/*  */
/* 1152 */	NdrFcShort( 0xffd8 ),	/* -40 */
/* 1154 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1156 */	0x0 , 
			0x0,		/* 0 */
/* 1158 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1162 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1166 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1168 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (1138) */
/* 1170 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1172 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1174 */	NdrFcShort( 0x28 ),	/* 40 */
/* 1176 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (1146) */
/* 1178 */	NdrFcShort( 0x0 ),	/* Offset= 0 (1178) */
/* 1180 */	0x6,		/* FC_SHORT */
			0x6,		/* FC_SHORT */
/* 1182 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1184 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1186 */	NdrFcShort( 0xfb7e ),	/* Offset= -1154 (32) */
/* 1188 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1190 */	0xb4,		/* FC_USER_MARSHAL */
			0x83,		/* 131 */
/* 1192 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1194 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1196 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1198 */	NdrFcShort( 0xfb6a ),	/* Offset= -1174 (24) */
/* 1200 */	0xb4,		/* FC_USER_MARSHAL */
			0x83,		/* 131 */
/* 1202 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1204 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1206 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1208 */	NdrFcShort( 0xfdca ),	/* Offset= -566 (642) */
/* 1210 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 1212 */	NdrFcShort( 0x6 ),	/* Offset= 6 (1218) */
/* 1214 */	
			0x13, 0x0,	/* FC_OP */
/* 1216 */	NdrFcShort( 0xfbba ),	/* Offset= -1094 (122) */
/* 1218 */	0xb4,		/* FC_USER_MARSHAL */
			0x83,		/* 131 */
/* 1220 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1222 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1224 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1226 */	NdrFcShort( 0xfff4 ),	/* Offset= -12 (1214) */

			0x0
        }
    };

static const USER_MARSHAL_ROUTINE_QUADRUPLE UserMarshalRoutines[ WIRE_MARSHAL_TABLE_SIZE ] = 
        {
            
            {
            LPSAFEARRAY_UserSize
            ,LPSAFEARRAY_UserMarshal
            ,LPSAFEARRAY_UserUnmarshal
            ,LPSAFEARRAY_UserFree
            },
            {
            BSTR_UserSize
            ,BSTR_UserMarshal
            ,BSTR_UserUnmarshal
            ,BSTR_UserFree
            }

        };



/* Standard interface: __MIDL_itf_LakesideLoungeOffice_0000_0000, ver. 0.0,
   GUID={0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}} */


/* Object interface: IUnknown, ver. 0.0,
   GUID={0x00000000,0x0000,0x0000,{0xC0,0x00,0x00,0x00,0x00,0x00,0x00,0x46}} */


/* Object interface: IDispatch, ver. 0.0,
   GUID={0x00020400,0x0000,0x0000,{0xC0,0x00,0x00,0x00,0x00,0x00,0x00,0x46}} */


/* Object interface: _IDTExtensibility2, ver. 0.0,
   GUID={0xB65AD801,0xABAF,0x11D0,{0xBB,0x8B,0x00,0xA0,0xC9,0x0F,0x27,0x44}} */

#pragma code_seg(".orpc")
static const unsigned short _IDTExtensibility2_FormatStringOffsetTable[] =
    {
    (unsigned short) -1,
    (unsigned short) -1,
    (unsigned short) -1,
    (unsigned short) -1,
    0,
    54,
    96,
    132,
    168
    };

static const MIDL_STUBLESS_PROXY_INFO _IDTExtensibility2_ProxyInfo =
    {
    &Object_StubDesc,
    LakesideLoungeOffice__MIDL_ProcFormatString.Format,
    &_IDTExtensibility2_FormatStringOffsetTable[-3],
    0,
    0,
    0
    };


static const MIDL_SERVER_INFO _IDTExtensibility2_ServerInfo = 
    {
    &Object_StubDesc,
    0,
    LakesideLoungeOffice__MIDL_ProcFormatString.Format,
    &_IDTExtensibility2_FormatStringOffsetTable[-3],
    0,
    0,
    0,
    0};
CINTERFACE_PROXY_VTABLE(12) __IDTExtensibility2ProxyVtbl = 
{
    &_IDTExtensibility2_ProxyInfo,
    &IID__IDTExtensibility2,
    IUnknown_QueryInterface_Proxy,
    IUnknown_AddRef_Proxy,
    IUnknown_Release_Proxy ,
    0 /* IDispatch::GetTypeInfoCount */ ,
    0 /* IDispatch::GetTypeInfo */ ,
    0 /* IDispatch::GetIDsOfNames */ ,
    0 /* IDispatch_Invoke_Proxy */ ,
    (void *) (INT_PTR) -1 /* _IDTExtensibility2::OnConnection */ ,
    (void *) (INT_PTR) -1 /* _IDTExtensibility2::OnDisconnection */ ,
    (void *) (INT_PTR) -1 /* _IDTExtensibility2::OnAddInsUpdate */ ,
    (void *) (INT_PTR) -1 /* _IDTExtensibility2::OnStartupComplete */ ,
    (void *) (INT_PTR) -1 /* _IDTExtensibility2::OnBeginShutdown */
};


static const PRPC_STUB_FUNCTION _IDTExtensibility2_table[] =
{
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    NdrStubCall2,
    NdrStubCall2,
    NdrStubCall2,
    NdrStubCall2,
    NdrStubCall2
};

CInterfaceStubVtbl __IDTExtensibility2StubVtbl =
{
    &IID__IDTExtensibility2,
    &_IDTExtensibility2_ServerInfo,
    12,
    &_IDTExtensibility2_table[-3],
    CStdStubBuffer_DELEGATING_METHODS
};


/* Object interface: IRibbonExtensibility, ver. 0.0,
   GUID={0x000C0396,0x0000,0x0000,{0xC0,0x00,0x00,0x00,0x00,0x00,0x00,0x46}} */

#pragma code_seg(".orpc")
static const unsigned short IRibbonExtensibility_FormatStringOffsetTable[] =
    {
    (unsigned short) -1,
    (unsigned short) -1,
    (unsigned short) -1,
    (unsigned short) -1,
    204
    };

static const MIDL_STUBLESS_PROXY_INFO IRibbonExtensibility_ProxyInfo =
    {
    &Object_StubDesc,
    LakesideLoungeOffice__MIDL_ProcFormatString.Format,
    &IRibbonExtensibility_FormatStringOffsetTable[-3],
    0,
    0,
    0
    };


static const MIDL_SERVER_INFO IRibbonExtensibility_ServerInfo = 
    {
    &Object_StubDesc,
    0,
    LakesideLoungeOffice__MIDL_ProcFormatString.Format,
    &IRibbonExtensibility_FormatStringOffsetTable[-3],
    0,
    0,
    0,
    0};
CINTERFACE_PROXY_VTABLE(8) _IRibbonExtensibilityProxyVtbl = 
{
    &IRibbonExtensibility_ProxyInfo,
    &IID_IRibbonExtensibility,
    IUnknown_QueryInterface_Proxy,
    IUnknown_AddRef_Proxy,
    IUnknown_Release_Proxy ,
    0 /* IDispatch::GetTypeInfoCount */ ,
    0 /* IDispatch::GetTypeInfo */ ,
    0 /* IDispatch::GetIDsOfNames */ ,
    0 /* IDispatch_Invoke_Proxy */ ,
    (void *) (INT_PTR) -1 /* IRibbonExtensibility::GetCustomUI */
};


static const PRPC_STUB_FUNCTION IRibbonExtensibility_table[] =
{
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    STUB_FORWARDING_FUNCTION,
    NdrStubCall2
};

CInterfaceStubVtbl _IRibbonExtensibilityStubVtbl =
{
    &IID_IRibbonExtensibility,
    &IRibbonExtensibility_ServerInfo,
    8,
    &IRibbonExtensibility_table[-3],
    CStdStubBuffer_DELEGATING_METHODS
};


/* Standard interface: __MIDL_itf_LakesideLoungeOffice_0000_0002, ver. 0.0,
   GUID={0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}} */

static const MIDL_STUB_DESC Object_StubDesc = 
    {
    0,
    NdrOleAllocate,
    NdrOleFree,
    0,
    0,
    0,
    0,
    0,
    LakesideLoungeOffice__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x60001, /* Ndr library version */
    0,
    0x801026e, /* MIDL Version 8.1.622 */
    0,
    UserMarshalRoutines,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };

const CInterfaceProxyVtbl * const _LakesideLoungeOffice_ProxyVtblList[] = 
{
    ( CInterfaceProxyVtbl *) &__IDTExtensibility2ProxyVtbl,
    ( CInterfaceProxyVtbl *) &_IRibbonExtensibilityProxyVtbl,
    0
};

const CInterfaceStubVtbl * const _LakesideLoungeOffice_StubVtblList[] = 
{
    ( CInterfaceStubVtbl *) &__IDTExtensibility2StubVtbl,
    ( CInterfaceStubVtbl *) &_IRibbonExtensibilityStubVtbl,
    0
};

PCInterfaceName const _LakesideLoungeOffice_InterfaceNamesList[] = 
{
    "_IDTExtensibility2",
    "IRibbonExtensibility",
    0
};

const IID *  const _LakesideLoungeOffice_BaseIIDList[] = 
{
    &IID_IDispatch,
    &IID_IDispatch,
    0
};


#define _LakesideLoungeOffice_CHECK_IID(n)	IID_GENERIC_CHECK_IID( _LakesideLoungeOffice, pIID, n)

int __stdcall _LakesideLoungeOffice_IID_Lookup( const IID * pIID, int * pIndex )
{
    IID_BS_LOOKUP_SETUP

    IID_BS_LOOKUP_INITIAL_TEST( _LakesideLoungeOffice, 2, 1 )
    IID_BS_LOOKUP_RETURN_RESULT( _LakesideLoungeOffice, 2, *pIndex )
    
}

const ExtendedProxyFileInfo LakesideLoungeOffice_ProxyFileInfo = 
{
    (PCInterfaceProxyVtblList *) & _LakesideLoungeOffice_ProxyVtblList,
    (PCInterfaceStubVtblList *) & _LakesideLoungeOffice_StubVtblList,
    (const PCInterfaceName * ) & _LakesideLoungeOffice_InterfaceNamesList,
    (const IID ** ) & _LakesideLoungeOffice_BaseIIDList,
    & _LakesideLoungeOffice_IID_Lookup, 
    2,
    2,
    0, /* table of [async_uuid] interfaces */
    0, /* Filler1 */
    0, /* Filler2 */
    0  /* Filler3 */
};
#pragma optimize("", on )
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* !defined(_M_IA64) && !defined(_M_AMD64) && !defined(_ARM_) */

