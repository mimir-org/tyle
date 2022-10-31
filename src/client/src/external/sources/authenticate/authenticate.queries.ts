import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { removeToken, setToken } from "common/utils/token";
import { toast } from "complib/data-display";
import { authenticateApi } from "external/sources/authenticate/authenticate.api";
import { userKeys } from "external/sources/user/user.queries";
import { useTranslation } from "react-i18next";
import { useMutation, useQueryClient } from "react-query";
import { useNavigate } from "react-router-dom";

export const useLogin = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgAuthenticateAm) => authenticateApi.postLogin(item), {
    onSuccess: (data) => {
      if (data) {
        setToken(data);
        queryClient.invalidateQueries(userKeys.all);
      }
    },
  });
};

export const useLogout = () => {
  const { t } = useTranslation();
  const queryClient = useQueryClient();
  const navigation = useNavigate();

  return useMutation(() => authenticateApi.postLogout(), {
    onSuccess: () => {
      removeToken();
      queryClient.invalidateQueries(userKeys.all);
      navigation(0);
    },
    onError: () => {
      toast.error(t("user.menu.logout.error"));
    },
  });
};
