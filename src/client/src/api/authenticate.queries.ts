import { useMutation, useQueryClient } from "@tanstack/react-query";
import { authenticateApi } from "api/authenticate.api";
import { userKeys } from "api/user.queries";
import { toast } from "components/Toaster/toast";
import { useNavigate } from "react-router-dom";
import { AuthenticateRequest } from "types/authentication/authenticateRequest";
import { removeToken, setToken } from "./token";

export const useLogin = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: AuthenticateRequest) => authenticateApi.postLogin(item),
    onSuccess: (data) => {
      if (data) {
        setToken(data);
        queryClient.invalidateQueries({ queryKey: userKeys.all });
      }
    },
  });
};

export const useLogout = () => {
  const queryClient = useQueryClient();
  const navigation = useNavigate();

  return useMutation({
    mutationFn: () => authenticateApi.postLogout(),
    onSuccess: () => {
      removeToken();
      queryClient.invalidateQueries({ queryKey: userKeys.all });
      navigation(0);
    },
    onError: () => {
      toast.error("Sorry! An error occurred during logout. Please contact support if the problem persists.");
    },
  });
};
