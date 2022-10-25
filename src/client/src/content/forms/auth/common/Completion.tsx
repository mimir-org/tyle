import { useTheme } from "styled-components";
import { Button } from "../../../../complib/buttons";
import { Box } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { Actionable } from "../../../../complib/types";
import { UnauthenticatedContent } from "../../../../features/ui/unauthenticated/layout/UnauthenticatedContent";

type CompletionProps = Partial<Actionable> & {
  title: string;
  infoText: string;
  complete?: Partial<Actionable>;
};

export const Completion = ({ title, infoText, complete }: CompletionProps) => {
  const theme = useTheme();

  return (
    <UnauthenticatedContent
      title={title}
      firstRow={
        <Box
          display={"flex"}
          flexDirection={"column"}
          alignItems={"center"}
          alignSelf={"center"}
          gap={theme.tyle.spacing.xxxl}
          maxWidth={"300px"}
        >
          <Text textAlign={"center"}>{infoText}</Text>
          {complete?.actionable && <Button onClick={complete.onAction}>{complete.actionText}</Button>}
        </Box>
      }
    />
  );
};
