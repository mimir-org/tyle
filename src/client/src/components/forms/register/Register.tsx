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
            <FormHeaderTitle>{TextResources.Register_Title}</FormHeaderTitle>
            <FormHeaderText>{TextResources.Register_Description}</FormHeaderText>
          </FormHeader>
          <FormInputCollection>
            <FormLabel htmlFor="email">{TextResources.Register_Email}</FormLabel>
            <Input id="email" type="email" {...register("email", { required: true })} />
            <FormError>{errors.email && errors.email.message}</FormError>

            <FormLabel htmlFor="firstName">{TextResources.Register_FirstName}</FormLabel>
            <Input id="firstName" {...register("firstName", { required: true })} />
            <FormError>{errors.firstName && errors.firstName.message}</FormError>

            <FormLabel htmlFor="lastName">{TextResources.Register_LastName}</FormLabel>
            <Input id="lastName" {...register("lastName", { required: true })} />
            <FormError>{errors.lastName && errors.lastName.message}</FormError>

            <FormLabel htmlFor="phoneNumber">{TextResources.Register_Phone}</FormLabel>
            <Input id="phoneNumber" type="tel" {...register("phoneNumber", { required: false })} />
            <FormError>{errors.phoneNumber && errors.phoneNumber.message}</FormError>

            <FormLabel htmlFor="password">{TextResources.Register_Password}</FormLabel>
            <Input id="password" type="password" {...register("password", { required: true })} />
            <FormError>{errors.password && errors.password.message}</FormError>

            <FormLabel htmlFor="confirmPassword">{TextResources.Register_Confirm_Password}</FormLabel>
            <Input id="confirmPassword" type="password" {...register("confirmPassword", { required: true })} />
            <FormError>{errors.confirmPassword && errors.confirmPassword.message}</FormError>
            <FormRequiredText>{TextResources.Forms_Required_Description}</FormRequiredText>
          </FormInputCollection>
          <FormActionContainer>
            <FormButton>{TextResources.Register_Submit}</FormButton>
            <FormSecondaryActionText>
              {TextResources.Register_Is_Registered} <FormLink to="/">{TextResources.Register_Login_Link}</FormLink>
            </FormSecondaryActionText>
          </FormActionContainer>
        </Form>
      )}
    </FormContainer>
  );
};
