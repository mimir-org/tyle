import { useForm } from "react-hook-form";
import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";
import { useLogin } from "../../../data/queries/auth/queriesAuthenticate";
import { getValidationStateFromServer } from "../../../data/helpers/getValidationStateFromServer";
import { useValidationFromServer } from "../../../hooks/useValidationFromServer";
import { Icon } from "../../../compLibrary/icon";
import { Input } from "../../../compLibrary/input";
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
  FormErrorBanner,
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
          <FormHeaderTitle>{TextResources.LOGIN_TITLE}</FormHeaderTitle>
          <FormHeaderText>{TextResources.LOGIN_DESCRIPTION}</FormHeaderText>
        </FormHeader>

        {loginMutation.isError && <FormErrorBanner>{TextResources.LOGIN_ERROR}</FormErrorBanner>}

        <FormInputCollection>
          <FormLabel htmlFor="email">{TextResources.LOGIN_EMAIL}</FormLabel>
          <Input
            id="email"
            type="email"
            placeholder={TextResources.FORMS_PLACEHOLDER_EMAIL}
            {...register("email", { required: true })}
          />
          <FormError>{errors.email && errors.email.message}</FormError>

          <FormLabel htmlFor="password">{TextResources.LOGIN_PASSWORD}</FormLabel>
          <Input
            id="password"
            type="password"
            placeholder={TextResources.FORMS_PLACEHOLDER_PASSWORD}
            {...register("password", { required: true })}
          />
          <FormError>{errors.password && errors.password.message}</FormError>

          <FormLabel htmlFor="code">{TextResources.LOGIN_CODE}</FormLabel>
          <Input
            id="code"
            type="tel"
            pattern="[0-9]*"
            autoComplete="off"
            placeholder={TextResources.FORMS_PLACEHOLDER_CODE}
            {...register("code", { required: true, valueAsNumber: true })}
          ></Input>
          <FormError>{errors.code && errors.code.message}</FormError>
          <FormRequiredText>{TextResources.FORMS_REQUIRED_DESCRIPTION}</FormRequiredText>
        </FormInputCollection>
        <FormActionContainer>
          <FormButton>{TextResources.LOGIN_TITLE}</FormButton>
          <FormSecondaryActionText>
            {TextResources.LOGIN_NOT_REGISTERED} <FormLink to="/register">{TextResources.LOGIN_REGISTER_LINK}</FormLink>
          </FormSecondaryActionText>
        </FormActionContainer>
      </Form>
    </FormContainer>
  );
};
