import { IChangePasswordRequest, IChangePasswordResponse, IGetProfileImageRequest, 
    IRegisterUserRequest,IUpdateUserRequest, IUpdateUserResponse, IRegisterUserResponse, 
    IUploadProfileImageRequest, IUploadProfileImageResponse, IValidateUserRequest, 
    IValidateUserResponse, ISearchUsersResponse} from "../types/apirequestresponsetypes/User";
import { ApiServiceBase } from "./ApiServiceBase";

class UserApiService extends ApiServiceBase {

    public async validateUser(user:IValidateUserRequest):Promise<IValidateUserResponse>{
        const response = await this.invoke<IValidateUserResponse>({method:'post', url:"/api/User/ValidateUser", data:  user});
        console.log('Response from validation')
        console.log(response);
        return response;
    }

    public async registerUser(user:IRegisterUserRequest):Promise<IRegisterUserResponse>{
        
        return await this.invoke<IRegisterUserResponse>({method:'post', url:"api/user/createuser", data : user});
    }

    public async changePassword(request:IChangePasswordRequest):Promise<IChangePasswordResponse>{
        return await this.invoke<IChangePasswordResponse>({method:'post', url:"/api/user/ChangePassword", data : request});
    }


    public async updateUser(user:IUpdateUserRequest):Promise<IUpdateUserResponse>{
        return await this.invoke<IUpdateUserResponse>({method:'post', url:"/user/api/Users/Update", data : user});
    }

    public async uploadProfileImage(request:IUploadProfileImageRequest):Promise<IUploadProfileImageResponse>{

        const formData = new FormData();
        formData.append('imageKey',request.imageKey);
        formData.append('file', request.file);

        return await this.invoke<IUploadProfileImageResponse>(
            {
                method:'post', 
                url:"user/api/Users/uploadprofileimage", 
                data : formData,
                headers: { "Content-Type": "multipart/form-data" }
            });
    }

    public async getProfileImage(request:IGetProfileImageRequest):Promise<Blob|null>{

        const response = await this.getBlob<Blob>({
            method:'get',
            'url':'user/api/Users/getprofileimage',
            params:{
                userName : request.userName
            },
            responseType: 'blob'
        })
       
        return response;
    }


    public async searchUsers(request:string):Promise<ISearchUsersResponse>{
        console.log(request);
        return await this.invoke<ISearchUsersResponse>(
            {
                method:'get', 
                url:"/api/User/searchuser", 
                params : 
                {
                    searchTerm:request
                }
            });
    }

}

export const userApiService = new UserApiService();