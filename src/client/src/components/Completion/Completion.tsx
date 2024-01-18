import AuthContent from "components/AuthContent";
import Box from "components/Box";
import Button from "components/Button";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { Actionable } from "types/actionable";

type CompletionProps = Partial<Actionable> & {
  title: string;
  infoText: string;
  complete?: Partial<Actionable>;
};

const Completion = ({ title, infoText, complete }: CompletionProps) => {
  const theme = useTheme();

  return (
    <AuthContent
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

export default Completion;
