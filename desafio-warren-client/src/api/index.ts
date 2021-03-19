import axios from 'axios';
import { authenticationProvider } from './authentication/authenticationProvider';

const instanceAxios = axios.create({
    baseURL: process.env.REACT_APP_API
})

const getHeaders = (token: string) => {
    return {
        headers: { Authorization: `Bearer ${token}`, Accept: 'application/json' }
    }
}

export async function getAsync<TResponse>(route: string): Promise<Response<TResponse>> {
    const token = await authenticationProvider.getAccessToken();

    const headers = getHeaders(token.accessToken);

    const response = await instanceAxios.get<Response<TResponse>>(route, headers);

    return response.data;
}

export async function postAsync<TResponse>(route: string, body: any): Promise<Response<TResponse>> {
    const token = await authenticationProvider.getAccessToken();

    const headers = getHeaders(token.accessToken);

    const response = await instanceAxios.post<Response<TResponse>>(route, body, headers);

    return response.data;
}

export async function putAsync<TResponse>(route: string, body: any): Promise<Response<TResponse>> {
    const token = await authenticationProvider.getAccessToken();

    const headers = getHeaders(token.accessToken);

    const response = await instanceAxios.put<Response<TResponse>>(route, body, headers);

    return response.data;
}

