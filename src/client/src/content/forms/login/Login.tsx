import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { LibraryIcon } from "../../../assets/icons/modules";
import { TextResources } from "../../../assets/text";
import { Button } from "../../../complib/buttons";
import { Form, FormErrorBanner, FormField, FormFieldset, FormHeader } from "../../../complib/form";
import { Input } from "../../../complib/inputs";
import { MotionFlexbox } from "../../../complib/layouts";
import { MotionIcon } from "../../../complib/media";
import { MotionText, Text } from "../../../complib/text";
import { getValidationStateFromServer } from "../../../data/helpers/getValidationStateFromServer";
import { useLogin } from "../../../data/queries/auth/queriesAuthenticate";
import { useValidationFromServer } from "../../../hooks/useValidationFromServer";
import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";
import { UnauthenticatedFormContainer } from "../styled/UnauthenticatedForm";

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
  const theme = useTheme();

  return (
    <UnauthenticatedFormContainer>
      <Form onSubmit={handleSubmit((data) => loginMutation.mutate(data))}>
        <MotionIcon layout size={50} src={LibraryIcon} alt="" />
        <FormHeader title={TextResources.LOGIN_TITLE} subtitle={TextResources.LOGIN_DESCRIPTION} />

        {loginMutation.isError && <FormErrorBanner>{TextResources.LOGIN_ERROR}</FormErrorBanner>}

        <FormFieldset>
          <FormField label={TextResources.LOGIN_EMAIL} error={errors.email}>
            <Input
              id="email"
              type="email"
              placeholder={TextResources.FORMS_PLACEHOLDER_EMAIL}
              {...register("email", { required: true })}
            />
          </FormField>

          <FormField label={TextResources.LOGIN_PASSWORD} error={errors.password}>
            <Input
              id="password"
              type="password"
              placeholder={TextResources.FORMS_PLACEHOLDER_PASSWORD}
              {...register("password", { required: true })}
            />
          </FormField>

          <FormField label={TextResources.LOGIN_CODE} error={errors.code}>
            <Input
              id="code"
              type="tel"
              pattern="[0-9]*"
              autoComplete="off"
              placeholder={TextResources.FORMS_PLACEHOLDER_CODE}
              {...register("code", { required: true, valueAsNumber: true })}
            />
          </FormField>

          <MotionText color={theme.tyle.color.sys.surface.variant.on} layout={"position"} as={"i"}>
            {TextResources.FORMS_REQUIRED_DESCRIPTION}
          </MotionText>
        </FormFieldset>

        <MotionFlexbox layout flexDirection={"column"} gap={theme.tyle.spacing.large}>
          <Button type={"submit"}>{TextResources.LOGIN_TITLE}</Button>
          <Text color={theme.tyle.color.sys.surface.variant.on}>
            {TextResources.LOGIN_NOT_REGISTERED} <Link to="/register">{TextResources.LOGIN_REGISTER_LINK}</Link>
          </Text>
        </MotionFlexbox>
      </Form>
    </UnauthenticatedFormContainer>
  );
};
