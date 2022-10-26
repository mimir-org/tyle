import { MimirorgQrCodeCm } from "@mimirorg/typelibrary-types";
import { useTheme } from "styled-components";
import { Button } from "../../../../complib/buttons";
import { Popover } from "../../../../complib/data-display";
import { Box, Flexbox } from "../../../../complib/layouts";
import { Icon } from "../../../../complib/media";
import { Text } from "../../../../complib/text";
import { Actionable } from "../../../../complib/types";
import { AuthContent } from "../auth-content/AuthContent";

interface MultiFactorAuthenticationProps {
  mfaInfo: MimirorgQrCodeCm;
  title?: string;
  infoText?: string;
  codeTitle?: string;
  manualCodeTitle?: string;
  manualCodeDescription?: string;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
}

export const MultiFactorAuthentication = (props: MultiFactorAuthenticationProps) => {
  const theme = useTheme();
  const { title, infoText } = props;
  const { codeTitle, manualCodeTitle, manualCodeDescription } = props;
  const { cancel, complete } = props;
  const { mfaInfo } = props;

  const showQrDialog = mfaInfo.code && mfaInfo.manualCode;

  return (
    <AuthContent
      title={title}
      firstRow={
        <>
          {showQrDialog && (
            <Flexbox as={"section"} flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.base}>
              <Text variant={"headline-small"}>{codeTitle}</Text>
              <Icon size={180} src={mfaInfo.code} alt="" />
              <Popover
                content={
                  <Box width={"230px"}>
                    <Text variant={"title-medium"}>{manualCodeDescription}</Text>
                    <Text variant={"body-medium"}>{mfaInfo.manualCode}</Text>
                  </Box>
                }
              >
                <Button variant={"text"}>{manualCodeTitle}</Button>
              </Popover>
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
