/*
 * Authentication - Trusted User.
 * Authorization - Accessibility of the particular resource.
 * 
 * 
 * Authentication Flow.
 * 
 * The user provides a username and pw.
 * The api checks if it valids and generates a token.
 * This token will be used as a secret pw for communication purposes.
 * 
 * 
 * Microsoft.AspNetCore.Authentication.JwtBearer
   Microsoft.IdentityModel.Tokens
   System.IdentityModel.Tokens.Jwt
   Microsoft.AspNetCore.Identity.EntityFrameworkCore
 * 
 * 
 * READER ROLE AND WRITER ROLE:
 * 1. GET - Reading purposes.
 * 2. PUT/POST/DELETE - Writing purposes.
 * 
 * 
 * CONNECT TO A NEW DB.
 * SEED ROLES AFTER CREATING ROLES in the auth db file.
 * In the program cs add the identity core module. 
 * 
 * CREATING AUTH CONTROLLER.
 * 
 * 
 * 
 * 
 */