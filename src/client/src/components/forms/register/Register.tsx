import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { getValidationStateFromServer } from "../../../data/helpers/getValidationStateFromServer";
import { useValidationFromServer } from "../../../hooks/useValidationFromServer";
import { TextResources } from "../../../assets/text";
import { Input } from "../../../compLibrary/input";
import { MimirorgUserAm } from "../../../models/auth/application/mimirorgUserAm";
import { useCreateUser } from "../../../data/queries/auth/queriesUser";
import { RegisterFinalize } from "./components/RegisterFinalize";
import { RegisterProcessing } from "./components/RegisterProcessing";
import { Icon } from "../../../compLibrary/icon";
import { LibraryIcon } from "../../../assets/icons/modules";
import { UnauthenticatedFormContainer } from "../styled/UnauthenticatedForm";
import { Form } from "../../../compLibrary/forms/Form";
import { FormErrorBanner } from "../../../compLibrary/forms/FormErrorBanner";
import { FormHeader } from "../../../compLibrary/forms/FormHeader";
import { FormField } from "../../../compLibrary/forms/FormField";
import { FormFieldset } from "../../../compLibrary/forms/FormFieldset";
import { Flex } from "../../../compLibrary/layout/Flex";
import { THEME } from "../../../compLibrary/core/constants";
import { Button } from "../../../compLibrary/button";

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
    <UnauthenticatedFormContainer>
      {createUserMutation.isLoading && <RegisterProcessing />}
      {createUserMutation.isSuccess && <RegisterFinalize qrCodeBase64={createUserMutation?.data?.code} />}
      {!createUserMutation.isSuccess && !createUserMutation.isLoading && (
        <Form onSubmit={handleSubmit((data) => createUserMutation.mutate(data))}>
          <Icon size={50} src={LibraryIcon} alt="" />
          <FormHeader title={TextResources.REGISTER_TITLE} subtitle={TextResources.REGISTER_DESCRIPTION} />

          {createUserMutation.isError && <FormErrorBanner>{TextResources.REGISTER_ERROR}</FormErrorBanner>}

          <FormFieldset>
            <FormField label={TextResources.REGISTER_EMAIL} error={errors.email}>
              <Input
                id="email"
                type="email"
                placeholder={TextResources.FORMS_PLACEHOLDER_EMAIL}
                {...register("email", { required: true })}
              />
            </FormField>

            <FormField label={TextResources.REGISTER_FIRSTNAME} error={errors.firstName}>
              <Input
                id="firstName"
                placeholder={TextResources.FORMS_PLACEHOLDER_FIRSTNAME}
                {...register("firstName", { required: true })}
              />
            </FormField>

            <FormField label={TextResources.REGISTER_LASTNAME} error={errors.lastName}>
              <Input
                id="lastName"
                placeholder={TextResources.FORMS_PLACEHOLDER_LASTNAME}
                {...register("lastName", { required: true })}
              />
            </FormField>

            <FormField label={TextResources.REGISTER_PHONE} error={errors.phoneNumber}>
              <Input id="phoneNumber" type="tel" {...register("phoneNumber", { required: false })} />
            </FormField>

            <FormField label={TextResources.REGISTER_PASSWORD} error={errors.password}>
              <Input
                id="password"
                type="password"
                placeholder={TextResources.FORMS_PLACEHOLDER_PASSWORD}
                {...register("password", { required: true })}
              />
            </FormField>

            <FormField label={TextResources.REGISTER_CONFIRM_PASSWORD} error={errors.confirmPassword}>
              <Input
                id="confirmPassword"
                type="password"
                placeholder={TextResources.FORMS_PLACEHOLDER_PASSWORD}
                {...register("confirmPassword", { required: true })}
              />
            </FormField>

            <i>{TextResources.FORMS_REQUIRED_DESCRIPTION}</i>
          </FormFieldset>
          <Flex flexDirection={"column"} gap={THEME.SPACING.LARGE}>
            <Button>{TextResources.REGISTER_SUBMIT}</Button>
            <p>
              {TextResources.REGISTER_IS_REGISTERED} <Link to="/">{TextResources.REGISTER_LOGIN_LINK}</Link>
            </p>
          </Flex>
        </Form>
      )}
    </UnauthenticatedFormContainer>
  );
};
