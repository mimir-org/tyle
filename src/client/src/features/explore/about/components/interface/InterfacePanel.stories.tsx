import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockInterfaceItem } from "common/utils/mocks";
import { InterfacePanel } from "features/explore/about/components/interface/InterfacePanel";

export default {
  title: "Features/Explore/About/InterfacePanel",
  component: InterfacePanel,
  args: {
    ...mockInterfaceItem(),
  },
} as ComponentMeta<typeof InterfacePanel>;

const Template: ComponentStory<typeof InterfacePanel> = (args) => <InterfacePanel {...args} />;

export const Default = Template.bind({});
