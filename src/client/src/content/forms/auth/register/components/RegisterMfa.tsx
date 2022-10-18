import { MimirorgQrCodeCm } from "@mimirorg/typelibrary-types";
import { useTheme } from "styled-components";
import { Button } from "../../../../../complib/buttons";
import { Flexbox } from "../../../../../complib/layouts";
import { Icon } from "../../../../../complib/media";
import { Text } from "../../../../../complib/text";
import { Actionable } from "../../../../../complib/types";
import { UnauthenticatedContent } from "../../../../app/components/unauthenticated/layout/UnauthenticatedContent";

type RegisterMfaProps = MimirorgQrCodeCm & {
  title?: string;
  infoText?: string;
  codeTitle?: string;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

export const RegisterMfa = (props: RegisterMfaProps) => {
  const theme = useTheme();
  const { title, infoText } = props;
  const { codeTitle, code, manualCode } = props;
  const { cancel, complete } = props;

  const showQrDialog = code && manualCode;

  return (
    <UnauthenticatedContent
      title={title}
      firstRow={
        <>
          {showQrDialog && (
            <Flexbox as={"section"} flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.base}>
              <Text variant={"headline-small"}>{codeTitle}</Text>
              <Icon size={180} src={code} alt="" />
            </Flexbox>
          )}
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{infoText}</Text>
          <Flexbox gap={theme.tyle.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} onClick={complete.onAction}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </>
      }
    />
  );
};
