import { DevTool } from "@hookform/devtools";
import { Actionable, Button, Input, Text } from "@mimirorg/component-library";
import { useGenerateMfa } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Digits from "components/Digits";
import Error from "components/Error";
import Flexbox from "components/Flexbox";
import MotionVerifyForm from "components/MotionVerifyForm";
import Processing from "components/Processing";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { Controller, useForm } from "react-hook-form";
import { useTheme } from "styled-components";
import { QrCodeView } from "types/authentication/qrCodeView";
import { VerifyRequest } from "types/authentication/verifyRequest";
import { onSubmitForm } from "./RecoverVerification.helpers";

type VerificationProps = Pick<VerifyRequest, "email"> & {
  setMfaInfo: (info: QrCodeView) => void;
  setVerificationInfo?: (info: VerifyRequest) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

const RecoverVerification = ({ email, setMfaInfo, setVerificationInfo, cancel, complete }: VerificationProps) => {
  const theme = useTheme();
  const { control, register, handleSubmit } = useForm<VerifyRequest>();

  const generateMfaMutation = useGenerateMfa();

  const showProcessing = generateMfaMutation.isLoading;
  const showError = generateMfaMutation.isError;
  const showInput = !showProcessing;

  useExecuteOnCriteria(complete?.onAction, generateMfaMutation.isSuccess);

  return (
    <AuthContent
      title="Verify e-mail"
      firstRow={
        <>
          {showProcessing && <Processing>Processing</Processing>}
          {showError && (
            <Error>
              We were not able verify your ownership of this email. Verify that that you have entered the correct code
              and please try again in about a minute. If the issue persist,
            </Error>
          )}
          {showInput && (
            <MotionVerifyForm
              id={"verify-form"}
              onSubmit={handleSubmit((data) =>
                onSubmitForm(data, generateMfaMutation.mutateAsync, setMfaInfo, setVerificationInfo),
              )}
              layout
            >
              <Input type={"hidden"} value={email} {...register("email")} />
              <Controller
                control={control}
                name={"code"}
                render={({ field: { value, onChange } }) => <Digits value={value} onChange={onChange} />}
              />
            </MotionVerifyForm>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>
            We have sent a six-digit code to your email. Please enter the code within 1 hour, or the code will expire
            and you will have to restart the recovery process. Make sure to check your spam/junk folder for the email.
          </Text>
          <Flexbox gap={theme.tyle.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} form={"verify-form"}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </>
      }
    />
  );
};

export default RecoverVerification;
