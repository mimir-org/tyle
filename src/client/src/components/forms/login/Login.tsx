import { useForm } from "react-hook-form";
import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";
import { useLogin } from "../../../data/queries/auth/queriesAuthenticate";
import { getValidationStateFromServer } from "../../../data/helpers/getValidationStateFromServer";
import { useValidationFromServer } from "../../../hooks/useValidationFromServer";
import { Icon } from "../../../compLibrary/icon";
import { Input } from "../../../compLibrary/input/text";
import { TextResources } from "../../../assets/text";
import { LibraryIcon } from "../../../assets/icons/modules";
import {
  Form,
  FormButton,
  FormError,
  FormHeaderTitle,
  FormHeader,
  FormHeaderText,
  FormInputCollection,
  FormLabel,
  FormLink,
  FormSecondaryActionText,
  FormContainer,
  FormActionContainer,
  FormRequiredText,
} from "../styled/Form";

export const Login = () => {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<MimirorgAuthenticateAm>();
  const loginMutation = useLogin();
  const validationState = getValidationStateFromServer<MimirorgAuthenticateAm>(loginMutation.error);
  useValidationFromServer<MimirorgAuthenticateAm>(setError, validationState?.errors);

  return (
    <FormContainer>
      <Form onSubmit={handleSubmit((data) => loginMutation.mutate(data))}>
        <Icon size={50} src={LibraryIcon} alt="" />
        <FormHeader>
          <FormHeaderTitle>{TextResources.Login_Title}</FormHeaderTitle>
          <FormHeaderText>{TextResources.Login_Description}</FormHeaderText>
        </FormHeader>

        <FormInputCollection>
          <FormLabel htmlFor="email">{TextResources.Login_Email}</FormLabel>
          <Input id="email" type="email" {...register("email", { required: true })} />
          <FormError>{errors.email && errors.email.message}</FormError>

          <FormLabel htmlFor="password">{TextResources.Login_Password}</FormLabel>
          <Input id="password" type="password" {...register("password", { required: true })} />
          <FormError>{errors.password && errors.password.message}</FormError>

          <FormLabel htmlFor="code">{TextResources.Login_Code}</FormLabel>
          <Input id="code" autoComplete="off" {...register("code", { required: true })}></Input>
          <FormError>{errors.code && errors.code.message}</FormError>
          <FormRequiredText>{TextResources.Forms_Required_Description}</FormRequiredText>
        </FormInputCollection>
        <FormActionContainer>
          <FormButton>{TextResources.Login_Title}</FormButton>
          <FormSecondaryActionText>
            {TextResources.Login_Not_Registered} <FormLink to="/register">{TextResources.Login_Register_Link}</FormLink>
          </FormSecondaryActionText>
        </FormActionContainer>
      </Form>
    </FormContainer>
  );
};
