import { ApiServiceBase } from './ApiServiceBase';
import {ISearchMoviesResponse} from "@/types/apirequestresponsetypes/Movie"
class MovieApiService extends ApiServiceBase {

    public async SearchMovies(searchTerm:string):Promise<ISearchMoviesResponse>{
        throw;
    } 
}
