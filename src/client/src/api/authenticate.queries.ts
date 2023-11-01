import { toast } from "@mimirorg/component-library";
import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { authenticateApi } from "api/authenticate.api";
import { removeToken, setToken } from "api/token";
import { userKeys } from "api/user.queries";
import { useTranslation } from "react-i18next";
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
  const { t } = useTranslation("ui");
  const queryClient = useQueryClient();
  const navigation = useNavigate();

  return useMutation(() => authenticateApi.postLogout(), {
    onSuccess: () => {
      removeToken();
      queryClient.invalidateQueries(userKeys.all);
      navigation(0);
    },
    onError: () => {
      toast.error(t("header.menu.logout.error"));
    },
  });
};
