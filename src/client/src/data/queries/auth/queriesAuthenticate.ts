import { useMutation, useQueryClient } from "react-query";
import { apiAuthenticate } from "../../api/auth/apiAuthenticate";
import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";
import { setToken } from "../../../utils/token";
import { userKeys } from "./queriesUser";
import { MimirorgTokenType } from "../../../models/auth/enums/mimirorgTokenType";

export const useLogin = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgAuthenticateAm) => apiAuthenticate.postLogin(item), {
    onSuccess: (data) => {
      const accessToken = data.find((x) => x.tokenType === MimirorgTokenType.AccessToken);
      if (accessToken) {
        setToken(accessToken);
        queryClient.invalidateQueries(userKeys.all);
      }
    },
  });
};
