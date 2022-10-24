import { MimirorgChangePasswordAm, MimirorgUserAm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery } from "react-query";
import { apiUser } from "../../api/auth/apiUser";

export const userKeys = {
  all: ["user"] as const,
};

export const useGetCurrentUser = () => {
  return useQuery(userKeys.all, apiUser.getCurrentUser, {
    retry: 0,
    refetchOnWindowFocus: false,
  });
};

export const useCreateUser = () => useMutation((item: MimirorgUserAm) => apiUser.postUser(item));

export const useVerification = () => useMutation((item: MimirorgVerifyAm) => apiUser.postVerification(item));

export const useGenerateMfa = () => useMutation((item: MimirorgVerifyAm) => apiUser.postGenerateMfa(item));

export const useChangePassword = () =>
  useMutation((item: MimirorgChangePasswordAm) => apiUser.postChangePassword(item));

export const useGenerateChangePasswordSecret = () =>
  useMutation((email: string) => apiUser.postGenerateChangePasswordSecret(email));
