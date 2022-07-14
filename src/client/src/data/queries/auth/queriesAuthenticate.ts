import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQueryClient } from "react-query";
import { setToken } from "../../../utils/token";
import { apiAuthenticate } from "../../api/auth/apiAuthenticate";
import { userKeys } from "./queriesUser";

export const useLogin = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgAuthenticateAm) => apiAuthenticate.postLogin(item), {
    onSuccess: (data) => {
      if (data) {
        setToken(data);
        queryClient.invalidateQueries(userKeys.all);
      }
    },
  });
};
