import { State } from "../../types/common/state";
import { useTheme } from "styled-components";
import { MotionBox } from "components/Box";
import PreviewPanel from "./PreviewPanel";

interface AttributePanelProps {
  name: string;
  description: string;
  state: State;
  kind: string;
}

const AttributePanel = ({ name, description, state, kind }: AttributePanelProps) => {
  const theme = useTheme();
  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.xxxl}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.tyle.animation.fade}
    >
      <PreviewPanel name={name} description={description} state={state} kind={kind} />
    </MotionBox>
  );
};

export default AttributePanel;
