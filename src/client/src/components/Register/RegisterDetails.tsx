import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { useCreateUser } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Button from "components/Button";
import { MotionFlexbox } from "components/Flexbox";
import Form from "components/Form";
import FormErrorBanner from "components/FormErrorBanner";
import FormField from "components/FormField";
import FormFieldset from "components/FormFieldset";
import Input from "components/Input";
import Processing from "components/Processing";
import Text, { MotionText } from "components/Text";
import Textarea from "components/Textarea";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { Actionable } from "types/actionable";
import { UserRequest } from "types/authentication/userRequest";
import { registerDetailsSchema } from "./registerDetailsSchema";

interface RegisterDetailsProps {
  setUserEmail: (email: string) => void;
  complete?: Partial<Actionable>;
}

const RegisterDetails = ({ complete, setUserEmail }: RegisterDetailsProps) => {
  const theme = useTheme();

  const mutation = useCreateUser();

  const formMethods = useForm({
    resolver: yupResolver(registerDetailsSchema()),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const onSubmit = (data: UserRequest) => {
    setUserEmail(data.email);
    mutation.mutate({
      ...data,
    });
  };

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <AuthContent
      title="Register"
      subtitle="Create an account to collaborate with your organization!"
      firstRow={
        <>
          {mutation.isPending && <Processing>Processing</Processing>}
          {!mutation.isSuccess && !mutation.isPending && (
            <Form id={"details-form"} onSubmit={handleSubmit((data) => onSubmit(data))}>
              {mutation.isError && (
                <FormErrorBanner>
                  We were not able to create your user at this moment. Please try again in about a minute. If the
                  problem persists, contact support.
                </FormErrorBanner>
              )}
              <FormFieldset>
                <FormField label="E-mail *" error={formState.errors.email}>
                  <Input id="email" type="email" placeholder="you@organization.com" {...register("email")} />
                </FormField>

                <FormField label="First name *" error={errors.firstName}>
                  <Input id="firstName" placeholder="Jane" {...register("firstName")} />
                </FormField>

                <FormField label="Last name *" error={errors.lastName}>
                  <Input id="lastName" placeholder="Smith" {...register("lastName")} />
                </FormField>

                <FormField label="Password *" error={errors.password}>
                  <Input id="password" type="password" placeholder="**********" {...register("password")} />
                </FormField>

                <FormField label="Confirm password *" error={errors.confirmPassword}>
                  <Input
                    id="confirmPassword"
                    type="password"
                    placeholder="**********"
                    {...register("confirmPassword")}
                  />
                </FormField>

                <FormField label="Purpose *" error={errors.purpose}>
                  <Textarea
                    placeholder="Please explain why should have access to the application."
                    {...register("purpose")}
                  />
                </FormField>

                <MotionText color={theme.tyle.color.surface.variant.on} layout={"position"} as={"i"}>
                  * Indicates a required field.
                </MotionText>
              </FormFieldset>
            </Form>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>
            Your next step will be to verify your e-mail and activate 2-factor authentication. Without these, you cannot
            access :Tyle. If this process is interrupted or you do not complete registration within 1 hour, you will
            have to start the registration process over again.
          </Text>
          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.xxl}>
            <Button type={"submit"} form={"details-form"}>
              {complete?.actionText}
            </Button>
            <Text color={theme.tyle.color.surface.variant.on}>
              Have an account? <Link to="/">Log in</Link>
            </Text>
          </MotionFlexbox>
        </>
      }
    />
  );
};

export default RegisterDetails;
