import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { Form } from "@mimirorg/component-library";
import { useGenerateChangePasswordSecret } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Button from "components/Button";
import Error from "components/Error";
import FormField from "components/FormField";
import Input from "components/Input";
import Processing from "components/Processing";
import Text from "components/Text";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { useForm } from "react-hook-form";
import { Actionable } from "types/actionable";
import { recoverDetailsSchema } from "./recoverDetailsSchema";

interface RecoverDetailsProps {
  setUserEmail: (email: string) => void;
  complete?: Partial<Actionable>;
}

const RecoverDetails = ({ complete, setUserEmail }: RecoverDetailsProps) => {
  const formMethods = useForm({
    resolver: yupResolver(recoverDetailsSchema()),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const mutation = useGenerateChangePasswordSecret();

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <AuthContent
      title="Account recovery"
      firstRow={
        <>
          {mutation.isLoading && <Processing>Processing</Processing>}
          {!mutation.isSuccess && !mutation.isLoading && (
            <Form
              id={"details-form"}
              onSubmit={handleSubmit((data) => {
                setUserEmail(data.email);
                mutation.mutate(data.email);
              })}
            >
              {mutation.isError && (
                <Error>
                  We were not able to start the recovery process. Please try again in about a minute. If the issue
                  persists,
                </Error>
              )}
              <FormField label="E-mail *" error={errors.email}>
                <Input id="email" type="email" placeholder="you@organization.com" {...register("email")} />
              </FormField>
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
            have to start the recovery process over again.
          </Text>
          <Button type={"submit"} form={"details-form"} alignSelf={"center"}>
            {complete?.actionText}
          </Button>
        </>
      }
    />
  );
};

export default RecoverDetails;
