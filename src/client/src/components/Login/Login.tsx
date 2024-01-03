import { yupResolver } from "@hookform/resolvers/yup";
import { Button, Form, FormErrorBanner, FormField, FormFieldset, Input } from "@mimirorg/component-library";
import { useLogin } from "api/authenticate.queries";
import AuthContent from "components/AuthContent";
import { MotionFlexbox } from "components/Flexbox";
import { recoverBasePath } from "components/Recover/RecoverRoutes";
import { registerBasePath } from "components/Register/RegisterRoutes";
import Text, { MotionText } from "components/Text";
import { useServerValidation } from "hooks/useServerValidation";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { useTheme } from "styled-components";
import { loginSchema } from "./loginSchema";

const Login = () => {
  const theme = useTheme();
  const navigate = useNavigate();

  const formMethods = useForm({
    resolver: yupResolver(loginSchema()),
  });

  const { register, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const mutation = useLogin();
  useServerValidation(mutation.error, setError);

  return (
    <AuthContent
      title="Login"
      subtitle="View, create and edit types!"
      firstRow={
        <Form onSubmit={handleSubmit((data) => mutation.mutate(data))}>
          {mutation.isError && <FormErrorBanner>Unable to login.</FormErrorBanner>}

          <FormFieldset>
            <FormField label="E-mail *" error={errors.email}>
              <Input id="email" type="email" placeholder="you@organization.com" {...register("email")} />
            </FormField>

            <FormField label="Password *" error={errors.password}>
              <Input id="password" type="password" placeholder="**********" {...register("password")} />
            </FormField>

            <FormField label="Authentication code *" error={errors.code}>
              <Input id="code" type="tel" autoComplete="off" placeholder="123456" {...register("code")} />
            </FormField>

            <MotionText color={theme.tyle.color.surface.variant.on} layout={"position"} as={"i"}>
              * Indicates a required field.
            </MotionText>
          </FormFieldset>
          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.xxl}>
            <Button type={"submit"}>Login</Button>
            <Text color={theme.tyle.color.surface.variant.on}>
              Don&apos;t have an account? <Link to={registerBasePath}>Sign up</Link>
            </Text>
          </MotionFlexbox>
        </Form>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>
            If you have forgotten your password, follow the instruction below for account recovery. You will need to
            re-verify your e-mail and authenticator. You will not lose the data you have created in :Tyle
          </Text>
          <Button variant={"outlined"} alignSelf={"center"} onClick={() => navigate(recoverBasePath)}>
            Account recovery
          </Button>
        </>
      }
    />
  );
};

export default Login;
