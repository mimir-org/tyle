import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { Button } from "../../../../complib/buttons";
import { Form, FormErrorBanner, FormField, FormFieldset, FormHeader } from "../../../../complib/form";
import { Input } from "../../../../complib/inputs";
import { MotionFlexbox } from "../../../../complib/layouts";
import { MotionText, Text } from "../../../../complib/text";
import { useLogin } from "../../../../data/queries/auth/queriesAuthenticate";
import { useServerValidation } from "../../../../hooks/useServerValidation";
import { MotionLogo } from "../../../common/logo/Logo";
import { UnauthenticatedFormContainer } from "../UnauthenticatedFormContainer";

export const Login = () => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "forms" });

  const { register, handleSubmit, setError, formState } = useForm<MimirorgAuthenticateAm>();
  const { errors } = formState;

  const mutation = useLogin();
  useServerValidation(mutation.error, setError);

  return (
    <UnauthenticatedFormContainer>
      <Form onSubmit={handleSubmit((data) => mutation.mutate(data))}>
        <MotionLogo layout width={"100px"} height={"50px"} inverse alt="" />
        <FormHeader title={t("login.title")} subtitle={t("login.description")} />

        {mutation.isError && <FormErrorBanner>{t("login.error")}</FormErrorBanner>}

        <FormFieldset>
          <FormField label={`${t("fields.email")} *`} error={errors.email}>
            <Input
              id="email"
              type="email"
              placeholder={t("placeholders.email")}
              {...register("email", { required: true })}
            />
          </FormField>

          <FormField label={`${t("fields.password")} *`} error={errors.password}>
            <Input
              id="password"
              type="password"
              placeholder={t("placeholders.password")}
              {...register("password", { required: true })}
            />
          </FormField>

          <FormField label={`${t("fields.code")} *`} error={errors.code}>
            <Input
              id="code"
              type="tel"
              pattern="[0-9]*"
              autoComplete="off"
              placeholder={t("placeholders.code")}
              {...register("code", { required: true, valueAsNumber: true })}
            />
          </FormField>

          <MotionText color={theme.tyle.color.sys.surface.variant.on} layout={"position"} as={"i"}>
            {t("placeholders.required")}
          </MotionText>
        </FormFieldset>

        <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.xxl}>
          <Button type={"submit"}>{t("login.submit")}</Button>
          <Text color={theme.tyle.color.sys.surface.variant.on}>
            {t("login.altLead")} <Link to="/register">{t("login.altLink")}</Link>
          </Text>
        </MotionFlexbox>
      </Form>
    </UnauthenticatedFormContainer>
  );
};
