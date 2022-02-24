import { useMutation, useQuery } from "react-query";
import { apiUser } from "../../api/auth/apiUser";
import { MimirorgUserAm } from "../../../models/auth/application/mimirorgUserAm";

const keys = {
  all: ["user"] as const,
};

export const useGetCurrentUser = () => useQuery(keys.all, apiUser.getCurrentUser);

export const useCreateUser = () => useMutation((item: MimirorgUserAm) => apiUser.postUser(item));
