import { MimirorgChangePasswordAm, MimirorgUserAm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery } from "@tanstack/react-query";
import { userApi } from "api/user.api";

export const userKeys = {
  all: ["user"] as const,
};

export const useGetCurrentUser = () =>
  useQuery(userKeys.all, userApi.getCurrentUser, {
    retry: 0,
    refetchOnWindowFocus: false,
  });

export const useCreateUser = () => useMutation((item: MimirorgUserAm) => userApi.postUser(item), {});

export const useUpdateUser = () => useMutation((item: MimirorgUserAm) => userApi.patchUser(item));

export const useVerification = () => useMutation((item: MimirorgVerifyAm) => userApi.postVerification(item));

export const useGenerateMfa = () => useMutation((item: MimirorgVerifyAm) => userApi.postGenerateMfa(item));

export const useChangePassword = () =>
  useMutation((item: MimirorgChangePasswordAm) => userApi.postChangePassword(item));

export const useGenerateChangePasswordSecret = () =>
  useMutation((email: string) => userApi.postGenerateChangePasswordSecret(email));
