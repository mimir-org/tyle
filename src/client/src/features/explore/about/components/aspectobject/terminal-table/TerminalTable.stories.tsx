import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockAspectObjectTerminalItem } from "common/utils/mocks";
import { TerminalTable } from "features/explore/about/components/aspectobject/terminal-table/TerminalTable";

export default {
  title: "Features/Common/Terminal/TerminalTable",
  component: TerminalTable,
  args: {
    terminals: [...Array(7)].map((_) => mockAspectObjectTerminalItem()),
  },
} as ComponentMeta<typeof TerminalTable>;

const Template: ComponentStory<typeof TerminalTable> = (args) => <TerminalTable {...args} />;

export const Default = Template.bind({});
