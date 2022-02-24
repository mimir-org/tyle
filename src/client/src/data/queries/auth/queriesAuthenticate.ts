import { useMutation } from "react-query";
import { apiAuthenticate } from "../../api/auth/apiAuthenticate";
import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";

export const useLogin = (item: MimirorgAuthenticateAm) => useMutation(() => apiAuthenticate.postLogin(item));
