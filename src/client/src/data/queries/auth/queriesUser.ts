import { useMutation, useQuery } from "react-query";
import { apiUser } from "../../api/auth/apiUser";
import { MimirorgUserAm } from "../../../models/auth/application/mimirorgUserAm";

export const userKeys = {
  all: ["user"] as const,
};

export const useGetCurrentUser = () => {
  return useQuery(userKeys.all, apiUser.getCurrentUser, {
    retry: 0,
  });
};

export const useCreateUser = () => useMutation((item: MimirorgUserAm) => apiUser.postUser(item));
