import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockInterfaceItem } from "../../../../../../utils/mocks";
import { InterfacePanel } from "./InterfacePanel";

export default {
  title: "Content/Explore/About/InterfacePanel",
  component: InterfacePanel,
  args: {
    ...mockInterfaceItem(),
  },
} as ComponentMeta<typeof InterfacePanel>;

const Template: ComponentStory<typeof InterfacePanel> = (args) => <InterfacePanel {...args} />;

export const Default = Template.bind({});
