import { yupResolver } from "@hookform/resolvers/yup";
import { MimirorgAuthenticateAm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { Button } from "complib/buttons";
import { Form, FormErrorBanner, FormField, FormFieldset } from "complib/form";
import { Input } from "complib/inputs";
import { MotionFlexbox } from "complib/layouts";
import { MotionText, Text } from "complib/text";
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
  const { t } = useTranslation();
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
            <FormField label={`${t("common.fields.email")} *`} error={errors.email}>
              <Input id="email" type="email" placeholder={t("common.placeholders.email")} {...register("email")} />
            </FormField>

            <FormField label={`${t("common.fields.password")} *`} error={errors.password}>
              <Input
                id="password"
                type="password"
                placeholder={t("common.placeholders.password")}
                {...register("password")}
              />
            </FormField>

            <FormField label={`${t("common.fields.code")} *`} error={errors.code}>
              <Input
                id="code"
                type="tel"
                autoComplete="off"
                placeholder={t("common.placeholders.code")}
                {...register("code")}
              />
            </FormField>

            <MotionText color={theme.tyle.color.sys.surface.variant.on} layout={"position"} as={"i"}>
              {t("common.placeholders.required")}
            </MotionText>
          </FormFieldset>
          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.xxl}>
            <Button type={"submit"}>{t("login.submit")}</Button>
            <Text color={theme.tyle.color.sys.surface.variant.on}>
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
