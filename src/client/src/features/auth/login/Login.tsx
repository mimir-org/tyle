import { yupResolver } from "@hookform/resolvers/yup";
import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { Button, Input, Form, FormErrorBanner, FormField, FormFieldset, MotionFlexbox, MotionText, Text } from "@mimirorg/component-library";
import { useLogin } from "external/sources/authenticate/authenticate.queries";
import { AuthContent } from "features/auth/common/auth-content/AuthContent";
import { loginSchema } from "features/auth/login/loginSchema";
import { recoverBasePath } from "features/auth/recover/RecoverRoutes";
import { registerBasePath } from "features/auth/register/RegisterRoutes";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link, useNavigate } from "react-router-dom";
import { useTheme } from "styled-components";

export const Login = () => {
  const theme = useTheme();
  const { t } = useTranslation("auth");
  const navigate = useNavigate();

  const formMethods = useForm<MimirorgAuthenticateAm>({
    resolver: yupResolver(loginSchema(t)),
  });

  const { register, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const mutation = useLogin();
  useServerValidation(mutation.error, setError);

  return (
    <AuthContent
      title={t("login.title")}
      subtitle={t("login.description")}
      firstRow={
        <Form onSubmit={handleSubmit((data) => mutation.mutate(data))}>
          {mutation.isError && <FormErrorBanner>{t("login.error")}</FormErrorBanner>}

          <FormFieldset>
            <FormField label={`${t("login.email")} *`} error={errors.email}>
              <Input id="email" type="email" placeholder={t("login.placeholders.email")} {...register("email")} />
            </FormField>

            <FormField label={`${t("login.password")} *`} error={errors.password}>
              <Input
                id="password"
                type="password"
                placeholder={t("login.placeholders.password")}
                {...register("password")}
              />
            </FormField>

            <FormField label={`${t("login.code")} *`} error={errors.code}>
              <Input
                id="code"
                type="tel"
                autoComplete="off"
                placeholder={t("login.placeholders.code")}
                {...register("code")}
              />
            </FormField>

            <MotionText color={theme.mimirorg.color.surface.variant.on} layout={"position"} as={"i"}>
              {t("login.placeholders.required")}
            </MotionText>
          </FormFieldset>
          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.mimirorg.spacing.xxl}>
            <Button type={"submit"}>{t("login.submit")}</Button>
            <Text color={theme.mimirorg.color.surface.variant.on}>
              {t("login.altLead")} <Link to={registerBasePath}>{t("login.altLink")}</Link>
            </Text>
          </MotionFlexbox>
        </Form>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("login.info.text")}</Text>
          <Button variant={"outlined"} alignSelf={"center"} onClick={() => navigate(recoverBasePath)}>
            {t("login.info.action")}
          </Button>
        </>
      }
    />
  );
};
