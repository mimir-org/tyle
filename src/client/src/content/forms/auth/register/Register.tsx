import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { Button } from "../../../../complib/buttons";
import { Form, FormErrorBanner, FormField, FormFieldset, FormHeader } from "../../../../complib/form";
import { Input } from "../../../../complib/inputs";
import { MotionFlexbox } from "../../../../complib/layouts";
import { MotionText, Text } from "../../../../complib/text";
import { getValidationStateFromServer } from "../../../../data/helpers/getValidationStateFromServer";
import { useCreateUser } from "../../../../data/queries/auth/queriesUser";
import { useValidationFromServer } from "../../../../hooks/useValidationFromServer";
import { MotionLogo } from "../../../common/Logo";
import { UnauthenticatedFormContainer } from "../UnauthenticatedFormContainer";
import { RegisterFinalize } from "./components/RegisterFinalize";
import { RegisterProcessing } from "./components/RegisterProcessing";

export const Register = () => {
  const theme = useTheme();
  const { t } = useTranslation();

  const { register, handleSubmit, setError, formState } = useForm<MimirorgUserAm>();
  const { errors } = formState;

  const createUserMutation = useCreateUser();
  const validationState = getValidationStateFromServer<MimirorgUserAm>(createUserMutation.error);
  useValidationFromServer<MimirorgUserAm>(setError, validationState?.errors);

  return (
    <UnauthenticatedFormContainer>
      {createUserMutation.isLoading && <RegisterProcessing />}
      {createUserMutation.isSuccess && <RegisterFinalize qrCodeBase64={createUserMutation?.data?.code} />}
      {!createUserMutation.isSuccess && !createUserMutation.isLoading && (
        <Form onSubmit={handleSubmit((data) => createUserMutation.mutate(data))}>
          <MotionLogo layout width={"100px"} height={"50px"} inverse alt="" />
          <FormHeader title={t("forms.register.title")} subtitle={t("forms.register.description")} />

          {createUserMutation.isError && <FormErrorBanner>{t("forms.register.error")}</FormErrorBanner>}

          <FormFieldset>
            <FormField label={`${t("forms.fields.email")} *`} error={formState.errors.email}>
              <Input
                id="email"
                type="email"
                placeholder={t("forms.placeholders.email")}
                {...register("email", { required: true })}
              />
            </FormField>

            <FormField label={`${t("forms.fields.firstname")} *`} error={errors.firstName}>
              <Input
                id="firstName"
                placeholder={t("forms.placeholders.firstname")}
                {...register("firstName", { required: true })}
              />
            </FormField>

            <FormField label={`${t("forms.fields.lastname")} *`} error={errors.lastName}>
              <Input
                id="lastName"
                placeholder={t("forms.placeholders.lastname")}
                {...register("lastName", { required: true })}
              />
            </FormField>

            <FormField label={t("forms.fields.phone")} error={errors.phoneNumber}>
              <Input id="phoneNumber" type="tel" {...register("phoneNumber", { required: false })} />
            </FormField>

            <FormField label={`${t("forms.fields.password")} *`} error={errors.password}>
              <Input
                id="password"
                type="password"
                placeholder={t("forms.placeholders.password")}
                {...register("password", { required: true })}
              />
            </FormField>

            <FormField
              label={`${t("common.confirm")} ${t("forms.fields.password").toLowerCase()} *`}
              error={errors.confirmPassword}
            >
              <Input
                id="confirmPassword"
                type="password"
                placeholder={t("forms.placeholders.password")}
                {...register("confirmPassword", { required: true })}
              />
            </FormField>

            <MotionText color={theme.tyle.color.sys.surface.variant.on} layout={"position"} as={"i"}>
              {t("forms.placeholders.required")}
            </MotionText>
          </FormFieldset>

          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.xxl}>
            <Button type={"submit"}>{t("forms.register.submit")}</Button>
            <Text color={theme.tyle.color.sys.surface.variant.on}>
              {t("forms.register.altLead")} <Link to="/">{t("forms.register.altLink")}</Link>
            </Text>
          </MotionFlexbox>
        </Form>
      )}
    </UnauthenticatedFormContainer>
  );
};
