import { useMutation, useQuery } from "@tanstack/react-query";
import { ChangePasswordRequest } from "types/authentication/changePasswordRequest";
import { UserRequest } from "types/authentication/userRequest";
import { VerifyRequest } from "types/authentication/verifyRequest";
import { userApi } from "./user.api";

export const userKeys = {
  all: ["user"] as const,
};

export const useGetCurrentUser = () =>
  useQuery(userKeys.all, userApi.getCurrentUser, {
    retry: 0,
    refetchOnWindowFocus: false,
  });

export const useCreateUser = () => useMutation((item: UserRequest) => userApi.postUser(item), {});

export const useUpdateUser = () => useMutation((item: UserRequest) => userApi.patchUser(item));

export const useVerification = () => useMutation((item: VerifyRequest) => userApi.postVerification(item));

export const useGenerateMfa = () => useMutation((item: VerifyRequest) => userApi.postGenerateMfa(item));

export const useChangePassword = () => useMutation((item: ChangePasswordRequest) => userApi.postChangePassword(item));

export const useGenerateChangePasswordSecret = () =>
  useMutation((email: string) => userApi.postGenerateChangePasswordSecret(email));
