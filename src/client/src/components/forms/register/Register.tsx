import { useForm } from "react-hook-form";
import { getValidationStateFromServer } from "../../../data/helpers/getValidationStateFromServer";
import { useValidationFromServer } from "../../../hooks/useValidationFromServer";
import { TextResources } from "../../../assets/text";
import { Input } from "../../../compLibrary/input/text";
import { MimirorgUserAm } from "../../../models/auth/application/mimirorgUserAm";
import { useCreateUser } from "../../../data/queries/auth/queriesUser";
import { RegisterFinalize } from "./components/RegisterFinalize";
import { RegisterProcessing } from "./components/RegisterProcessing";
import { Icon } from "../../../compLibrary/icon";
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

export const Register = () => {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<MimirorgUserAm>();
  const createUserMutation = useCreateUser();
  const validationState = getValidationStateFromServer<MimirorgUserAm>(createUserMutation.error);
  useValidationFromServer<MimirorgUserAm>(setError, validationState?.errors);

  return (
    <FormContainer>
      {createUserMutation.isLoading && <RegisterProcessing />}
      {createUserMutation.isSuccess && <RegisterFinalize qrCodeBase64={createUserMutation?.data?.code} />}
      {!createUserMutation.isSuccess && !createUserMutation.isLoading && (
        <Form onSubmit={handleSubmit((data) => createUserMutation.mutate(data))}>
          <Icon size={50} src={LibraryIcon} alt="" />
          <FormHeader>
            <FormHeaderTitle>{TextResources.REGISTER_TITLE}</FormHeaderTitle>
            <FormHeaderText>{TextResources.REGISTER_DESCRIPTION}</FormHeaderText>
          </FormHeader>

          {createUserMutation.isError && <FormErrorBanner>{TextResources.REGISTER_ERROR}</FormErrorBanner>}

          <FormInputCollection>
            <FormLabel htmlFor="email">{TextResources.REGISTER_EMAIL}</FormLabel>
            <Input
              id="email"
              type="email"
              placeholder={TextResources.FORMS_PLACEHOLDER_EMAIL}
              {...register("email", { required: true })}
            />
            <FormError>{errors.email && errors.email.message}</FormError>

            <FormLabel htmlFor="firstName">{TextResources.REGISTER_FIRSTNAME}</FormLabel>
            <Input
              id="firstName"
              placeholder={TextResources.FORMS_PLACEHOLDER_FIRSTNAME}
              {...register("firstName", { required: true })}
            />
            <FormError>{errors.firstName && errors.firstName.message}</FormError>

            <FormLabel htmlFor="lastName">{TextResources.REGISTER_LASTNAME}</FormLabel>
            <Input
              id="lastName"
              placeholder={TextResources.FORMS_PLACEHOLDER_LASTNAME}
              {...register("lastName", { required: true })}
            />
            <FormError>{errors.lastName && errors.lastName.message}</FormError>

            <FormLabel htmlFor="phoneNumber">{TextResources.REGISTER_PHONE}</FormLabel>
            <Input id="phoneNumber" type="tel" {...register("phoneNumber", { required: false })} />
            <FormError>{errors.phoneNumber && errors.phoneNumber.message}</FormError>

            <FormLabel htmlFor="password">{TextResources.REGISTER_PASSWORD}</FormLabel>
            <Input
              id="password"
              type="password"
              placeholder={TextResources.FORMS_PLACEHOLDER_PASSWORD}
              {...register("password", { required: true })}
            />
            <FormError>{errors.password && errors.password.message}</FormError>

            <FormLabel htmlFor="confirmPassword">{TextResources.REGISTER_CONFIRM_PASSWORD}</FormLabel>
            <Input
              id="confirmPassword"
              type="password"
              placeholder={TextResources.FORMS_PLACEHOLDER_PASSWORD}
              {...register("confirmPassword", { required: true })}
            />
            <FormError>{errors.confirmPassword && errors.confirmPassword.message}</FormError>
            <FormRequiredText>{TextResources.FORMS_REQUIRED_DESCRIPTION}</FormRequiredText>
          </FormInputCollection>
          <FormActionContainer>
            <FormButton>{TextResources.REGISTER_SUBMIT}</FormButton>
            <FormSecondaryActionText>
              {TextResources.REGISTER_IS_REGISTERED} <FormLink to="/">{TextResources.REGISTER_LOGIN_LINK}</FormLink>
            </FormSecondaryActionText>
          </FormActionContainer>
        </Form>
      )}
    </FormContainer>
  );
};
