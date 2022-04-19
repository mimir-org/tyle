import { ComponentMeta } from "@storybook/react";
import { Default as Icon } from "../../media/icon/Icon.stories";
import { Default as VisuallyHidden } from "../../accessibility/hidden/VisuallyHidden.stories";
import { Button } from "./Button";

export default {
  title: "Buttons/Button",
  component: Button,
} as ComponentMeta<typeof Button>;

export const Default = () => <Button>{"Button"}</Button>;

export const Disabled = () => <Button disabled>{"Button"}</Button>;

export const AsAnchor = () => <Button as={"a"}>{"Button"}</Button>;

export const WithIcon = () => (
  <Button>
    <Icon {...Icon.args} />
    <VisuallyHidden {...VisuallyHidden.args} />
  </Button>
);

export const WithIconAndTextLeft = () => (
  <Button>
    <Icon {...Icon.args} />
    <span>Button</span>
  </Button>
);

export const WithIconAndTextRight = () => (
  <Button>
    <span>Button</span>
    <Icon {...Icon.args} />
  </Button>
);
