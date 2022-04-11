import { useMutation, useQueryClient } from "react-query";
import { apiAuthenticate } from "../../api/auth/apiAuthenticate";
import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";
import { setToken } from "../../../utils/token";
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
