import { MimirorgChangePasswordAm, MimirorgUserAm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery } from "@tanstack/react-query";
import { userApi } from "external/sources/user/user.api";

export const userKeys = {
  all: ["user"] as const,
  allPending: ["usersPending"] as const,
  pendingLists: () => [...userKeys.allPending, "list"] as const,
};

export const useGetCurrentUser = () =>
  useQuery(userKeys.all, userApi.getCurrentUser, {
    retry: 0,
    refetchOnWindowFocus: false,
  });

export const useCreateUser = () => useMutation((item: MimirorgUserAm) => userApi.postUser(item), {});

export const useGetPendingUsers = (companyId?: string) =>
  useQuery(userKeys.pendingLists(), () => userApi.getPendingUsers(companyId), { enabled: !!companyId, retry: false });

export const useVerification = () => useMutation((item: MimirorgVerifyAm) => userApi.postVerification(item));

export const useGenerateMfa = () => useMutation((item: MimirorgVerifyAm) => userApi.postGenerateMfa(item));

export const useChangePassword = () =>
  useMutation((item: MimirorgChangePasswordAm) => userApi.postChangePassword(item));

export const useGenerateChangePasswordSecret = () =>
  useMutation((email: string) => userApi.postGenerateChangePasswordSecret(email));
